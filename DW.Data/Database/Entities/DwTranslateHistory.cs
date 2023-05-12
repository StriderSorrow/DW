using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwTranslateHistory
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DwUser Updater { get; set; }
        public DwScriptLine Line { get; set; }
    }
}
