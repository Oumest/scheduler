using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scheduler_v2.Models.DTOs
{
    public class TeamDTO
    {
        public string TeamName { get; set; }
        public int Teamlead_id { get; set; }
        public int[] Users { get; set; }
    }
}