using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwCharacter
    {
        public DwCharacter()
        {
            Candidates ??= new HashSet<DwUser>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DwUser? Actor { get; set; }
        public DwProject Project { get; set; }

        public ICollection<DwUser> Candidates { get; set; }
        public ICollection<DwMedia> Medias { get; set; }
    }
}
