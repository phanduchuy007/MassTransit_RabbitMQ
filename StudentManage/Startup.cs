using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Model;
using RabbitMQ.Client;
using StudentManage.Dal.Repository.Interface;
using StudentManage.Dal.Repository.StudentRepository;
using StudentManage.Dal.Repository.SubjectjRepository;
using StudentManage.Dal.UnitOfWork;
using StudentManage.Models;
using StudentManage.Services;

namespace StudentManage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => {
                // options.AddDefaultPolicy(builder => builder.AllowAnyOrigin());
                // options.AddDefaultPolicy(builder => builder.WithOrigins("https://localhost:44380"));
                options.AddPolicy("mypolicy", builder => builder.WithOrigins("https://localhost:44380"));
            });

            services.AddDbContextPool<StudentDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StudentConnection")));

            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddScoped<ISubjectsRepository, SubjectsRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<Service>();

            // MassTransit
            services.AddMassTransit(config =>
            {
                //config.AddConsumer<CourseRegistrationConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.Message<ListOperationModel>(e => e.SetEntityName("list-report-requests")); // name of the primary exchange
                    cfg.Publish<ListOperationModel>(e => e.ExchangeType = ExchangeType.Direct); // primary exchange type
                    cfg.Send<ListOperationModel>(e =>
                    {
                        e.UseRoutingKeyFormatter(ctx => ctx.Message.Provider.ToString());
                    });

                   /* cfg.ReceiveEndpoint("student-subject-queue", c =>
                    {
                        c.ConfigureConsumer<CourseRegistrationConsumer>(ctx);
                    });*/
                });
                config.AddRequestClient<ListOperationModel>();
            });

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
