using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Model;

namespace StudentManage.Services
{
    public class StudentConsumer : IConsumer<OperationModel>
    {
        public async Task Consume(ConsumeContext<OperationModel> context)
        {
            
        }
    }
}
