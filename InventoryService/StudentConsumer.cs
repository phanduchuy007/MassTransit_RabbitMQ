using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Model;

namespace InventoryService
{
    public class StudentConsumer : IConsumer<ListOperationModel>
    {
        public async Task Consume(ConsumeContext<ListOperationModel> context)
        {
            var list = context?.Message?.operations.ToList();

            if (list.Count > 0)
            {
                var operation = new OperationModel()
                {
                    Name = list[0].Name,
                    Address = list[0].Address,
                    Email = list[0].Email,
                    Subject = list[0].Subject,
                    Teacher = list[0].Teacher,
                    Classroom = list[0].Classroom,
                    Mark = list[0].Mark
                };

                foreach (var item in context.Message.operations)
                {
                    if (item.Mark >= operation.Mark)
                    {
                        operation.Name = item.Name;
                        operation.Address = item.Address;
                        operation.Email = item.Email;
                        operation.Subject = item.Subject;
                        operation.Teacher = item.Teacher;
                        operation.Classroom = item.Classroom;
                        operation.Mark = item.Mark;
                    }
                }
                if (operation != null)
                {
                }
            }

            await context.RespondAsync<OperationModel>(new
            {
                Name = "operation.Name",
                Address = "operation.Address",
                Email = "operation.Email",
                Subject = "operation.Subject",
                Teacher = "operation.Teacher",
                Classroom = "operation.Classroom",
                Mark = "operation.Mark"
            }); ;
            /*return Task.CompletedTask;*/
        }
    }
}
/*await context.RespondAsync<OperationModel>(new
{
    operation.Name,
    operation.Address,
    operation.Email,
    operation.Subject,
    operation.Teacher,
    operation.Classroom,
    operation.Mark
});*/