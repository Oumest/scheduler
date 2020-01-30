using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scheduler_v2.Models.DTOs
{
    public class CustomerDTO
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Comment { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Fixed_rate { get; set; }
        public string Hourly_rate { get; set; }
        public string  Country { get; set; }
        public string Currency { get; set; }
        public int Budget { get; set; }
        public int TimeBudget { get; set; }
    }
}