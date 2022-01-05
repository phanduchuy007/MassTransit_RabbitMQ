using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Model;

namespace StudentManage.Services
{
    public class CourseRegistrationConsumer : IConsumer<OperationModel>
    {
        Service _services;
        private readonly ILogger<CourseRegistrationConsumer> _logger;

        public CourseRegistrationConsumer(ILogger<CourseRegistrationConsumer> logger, Service service)
        {
            _services = service;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OperationModel> context)
        {
            /*_logger.LogInformation("Value: {Value}", context.Message);
            _services.AddDataTable((Operation)context.Message);*/
            await Console.Out.WriteLineAsync(context.Message.Classroom);
            _logger.LogInformation($"Got new message {context.Message}");
        }
    }
}
