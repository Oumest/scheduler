using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scheduler_v2.Models.DTOs
{
    public class TimeSheetDTO
    {
        public int User_id { get; set; }
        public int Project_id { get; set; }
        public int Activity_id { get; set; }
        public DateTime Start_time { get; set; }
        public string Start_date { get; set; }
        public string  End_time { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public short Exported { get; set; }
        public double Fixed_rate { get; set; }
        public double Hourly_rate { get; set; }
    }
}