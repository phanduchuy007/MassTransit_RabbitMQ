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
            await context.RespondAsync<OperationModel>(new
            {
                Name = context.Message.operations[0].Name,
                Address = context.Message.operations[0].Address,
                Email = context.Message.operations[0].Email,
                Subject = context.Message.operations[0].Subject,
                Teacher = context.Message.operations[0].Teacher,
                Classroom = context.Message.operations[0].Classroom,
                Mark = context.Message.operations[0].Mark
            });
            /*return Task.CompletedTask;*/
        }
    }
}
