using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwTask
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateOnly Deadline { get; set; }
        public DwUser Executor { get; set; }
    }

}
