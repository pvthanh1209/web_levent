using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_levent.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
    public class MemberSession
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Role { get; set; } 
        // Note: role = 1 là admin, role = 2 là user
    }

    [Serializable]
    public class SessionCustomer
    {
        public static string sessionName = "customer";
        public static MemberSession GetUser()
        {

            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null &&
                HttpContext.Current.Session.Count > 0 &&
                HttpContext.Current.Session[SessionCustomer.sessionName] != null)
            {
                return HttpContext.Current.Session[SessionCustomer.sessionName] as MemberSession;
            }
            return null;
        }

        /// <summary>
        /// Sets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool SetUser(MemberSession user)
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(SessionCustomer.sessionName);
            }
            HttpContext.Current.Session.Add(SessionCustomer.sessionName, user);
            return true;
        }

        /// <summary>
        /// Clears the session.
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Remove("customer");
        }
    }
}