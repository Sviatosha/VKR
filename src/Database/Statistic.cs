using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string clicksInSecondJson
        {
            get { return JsonConvert.SerializeObject(this.clicksInSecond); }
            set { this.clicksInSecond = JsonConvert.DeserializeObject<int[]>(value); }
        }
        [NotMapped]
        public int[] clicksInSecond { get; set; }
    }
}
