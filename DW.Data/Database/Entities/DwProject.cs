using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwProject
    {
        public DwProject()
        {
            Participants ??= new HashSet<DwUser>();
            ScriptLines ??= new HashSet<DwScriptLine>();
            Tasks ??= new HashSet<DwTask>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly? Deadline { get; set; }
        public DwTeam Team { get; set; }
        public DwUser Creator { get; set; }
        public DwUser Leader { get; set; }
        public ICollection<DwUser> Participants { get; set; }
        public ICollection<DwScriptLine> ScriptLines { get; set; }
        public ICollection<DwTask> Tasks { get; set; }
        public ICollection<DwMedia> Medias { get; set; }
    }
}
