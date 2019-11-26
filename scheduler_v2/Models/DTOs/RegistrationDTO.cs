using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scheduler_v2.Models.DTOs
{
    public class RegistrationDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}