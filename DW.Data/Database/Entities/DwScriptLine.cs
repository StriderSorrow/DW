using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwScriptLine
    {
        public DwScriptLine()
        {
            History ??= new HashSet<DwTranslateHistory>();
        }
        public long Id { get; set; }
        public bool IsTimed { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
        public DwCharacter? Character { get; set; }
        public DwProject Project { get; set; }

        public ICollection<DwTranslateHistory> History { get; set; }
    }
}
