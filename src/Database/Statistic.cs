using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR.src.Database
{
    internal class Statistic
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string type { get; set; }
        public int seconds { get; set; }
        public int errors { get; set; }
        public int letters_count { get; set; }
        public List<int> clicks_in_second { get; set; }
    }
}
