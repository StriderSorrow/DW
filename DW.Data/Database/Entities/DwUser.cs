using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwUser : IdentityUser
    {
        public DwUser()
        {
            Teams ??= new HashSet<DwTeam>();
            Projects ??= new HashSet<DwProject>();
            Characters ??= new HashSet<DwCharacter>();
            Tasks ??= new HashSet<DwTask>();
        }
        public string Id { get; set; }
        public string Nickname { get; set; }
        public bool IsDeleted { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
        public string? Town {get; set; }
        public ICollection<DwTeam> Teams { get; set; }
        public ICollection<DwProject> Projects { get; set; }
        public ICollection<DwCharacter> Candidate { get; set; }
        public ICollection<DwCharacter> Characters { get; set; }
        public ICollection<DwTask> Tasks { get; set; }
        public ICollection<DwMedia> Medias { get; set; }
        public ICollection<DwTeam> CreatedTeams { get; set; }
        public ICollection<DwTeam> LeadingTeams { get; set; }
        public ICollection<DwProject> LeadingProjects { get; set; }
        public ICollection<DwProject> CreatedProjects { get; set; }


    }

}
