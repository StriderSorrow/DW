using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database.Entities
{
    public class DwUserConfirmation
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string MailCode { get; set; } = RandomString(5);
        public DwUser User { get; set; }
        private static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
