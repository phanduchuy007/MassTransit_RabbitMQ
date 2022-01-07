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
            var list = context?.Message?.Provider; //vì thằng operations null nên h mình lấy provider thôi

            /* if (list.Count > 0)
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

                 var max = list.Max(x => x.Mark);
                 var operationMax = list.Where(x => x.Mark == max);

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
                     await context.RespondAsync<OperationModel>(new
                     {
                         Provider = "list",
                          // OperationMax = operationMax
                          Name = operation.Name,
                          Address = operation.Address,
                          Email = operation.Email,
                          Subject = operation.Subject,
                          Teacher = operation.Teacher,
                          Classroom = operation.Classroom,
                          Mark = operation.Mark
                      }); ;
                 }
             }*/
            await context.RespondAsync<OperationModel>(new OperationModel
            {
                Name = "1",
                Address= "2",
                Email= "4",
                Subject= "3",
                Teacher= "5",
                Classroom= "6",
                Mark= 7
            });
            /*return Task.CompletedTask;*/
        }
    }
}