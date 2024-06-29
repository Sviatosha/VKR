using Newtonsoft.Json;
using System;
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

        public string clicksInMinuteJson
        {
            get { return JsonConvert.SerializeObject(this.clicksInMinute); }
            set { this.clicksInMinute = JsonConvert.DeserializeObject<int[]>(value); }
        }
        [NotMapped]
        public int[] clicksInMinute { get; set; }
    }
}
