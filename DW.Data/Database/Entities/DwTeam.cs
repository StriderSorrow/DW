using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwTeam
    {
        public DwTeam()
        {
            Users ??= new HashSet<DwUser>();
            Projects ??= new HashSet<DwProject>();
            Tasks ??= new HashSet<DwTask>();
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DwUser Creator { get; set; }
        public DwUser Leader { get; set; }

        public ICollection<DwUser> Users { get; set; }
        public ICollection<DwProject> Projects { get; set; }
        public ICollection<DwTask> Tasks { get; set; }
        public ICollection<DwMedia> Medias { get; set; }
    }
}
