using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_levent.Models;

namespace web_levent.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        LeventEntities db = new LeventEntities();
        public MemberSession memberSession;
        public BaseController()
        {
            memberSession = SessionCustomer.GetUser();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (memberSession == null || memberSession.UserId == 0)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
            else if (memberSession != null)
            {
                var dataAccount = db.AdminUsers.Where(x => x.ID_User == memberSession.UserId).FirstOrDefault();
                if (memberSession.UserId != dataAccount.ID_User)
                {
                    filterContext.Result = new RedirectResult("/Login/Logout");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}