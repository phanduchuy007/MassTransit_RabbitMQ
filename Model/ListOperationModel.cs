using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ListOperationModel
    {
        public string Provider { get; set; }
        public List<OperationModel> operations { get; set; }
    }
}
