using System;
using System.Collections.Generic;

namespace BetoApp.Models
{
    public class Alarm
    {
        public string Id { get; set; }
        public string Items { get; set; }
        public TimeSpan Hour { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public int NotificationId { get; set; }
    }
}
