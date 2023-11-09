using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_levent.Models
{
    public class ChangePassword
    {
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string ConfirmPassword { get; set; }
    }
}