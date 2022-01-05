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
            var list = context.Message.operations;
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
            await context.RespondAsync<OperationModel>(operation);
            /*return Task.CompletedTask;*/
        }
    }
}
