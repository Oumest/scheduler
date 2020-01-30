using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scheduler_v2.Models.DTOs
{
    public class ProjectDTO
    {
        public int Customer_id { get; set; }
        public string Name { get; set; }
        public string Order_number { get; set; }
        public string Comment { get; set; }
        public int Visible { get; set; }
        public float Fixed_rate { get; set; }
        public float Hourly_rate { get; set; }
        public float Budget { get; set; }
        public int Time_budget { get; set; }
        public string Color { get; set; }
    }
}