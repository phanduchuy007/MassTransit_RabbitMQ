using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManage.Models
{
    public class Operation
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Classroom { get; set; }
        public double? Mark { get; set; }
    }
}
