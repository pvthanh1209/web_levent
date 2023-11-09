using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_levent.Models
{
    public class RegistorModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
    }
}