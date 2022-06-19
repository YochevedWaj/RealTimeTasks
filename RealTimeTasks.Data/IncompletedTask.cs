using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeTasks.Data
{
    public class IncompletedTask
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
    }
}
