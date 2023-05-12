using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwMedia
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Path { get; set; }
        public DwUser Uploader { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
