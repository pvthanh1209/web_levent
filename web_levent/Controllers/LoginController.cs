using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_levent.Models;

namespace web_levent.Controllers
{
    public class LoginController : Controller
    {
        LeventEntities db = new LeventEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            var password = Helper.ToMD5(model.Password);
            var dataUser = db.AdminUsers.Where(x => x.User_Name.ToLower().Trim().Equals(model.Username.ToLower().Trim()) && x.Password_User.Equals(password)).FirstOrDefault();
            if(dataUser == null)
            {
                ViewBag.Error = "Tài khoản hoặc mật khẩu không chính xác";
                return View();
            }
            var sess = new MemberSession
            {
                UserId = dataUser.ID_User,
                Username = dataUser.User_Name,
                Email = dataUser.Email_User,
                Phone = dataUser.Phone_Number,
                Role = dataUser.Role.Value
            };
            SessionCustomer.sessionName = "customer";
            SessionCustomer.SetUser(sess);
            if(dataUser.Role.Value == 1)
            {
                return Redirect("/Admin/Home");
            }
            else
            {
                return RedirectToAction("Index", "Shop");
            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegistorModel model)
        {
            var password = Helper.ToMD5(model.Password);
            if (!model.Password.Equals(model.RePassword))
            {
                ViewBag.Error = "Mật khẩu xác nhận không đúng";
                return View();
            }
            var fullName = db.AdminUsers.Where(x => x.User_Name.ToLower().Equals(model.Username.ToLower().Trim())).FirstOrDefault();
            if(fullName != null)
            {
                ViewBag.Error = "Tài khoản đã tồn tại";
                return View();
            }
            var entity = new AdminUser
            {
                ID_User = 0,
                User_Name = model.Username,
                Full_Name = model.FullName,
                Email_User = model.Email,
                Phone_Number = model.Phone,
                Address = model.Address,
                Password_User = password,
                Role = 2
            };
            db.AdminUsers.Add(entity);
            db.SaveChanges();
            ViewBag.Success = "Đăng ký thành công";
            return View();
        }
        public ActionResult Logout()
        {
            SessionCustomer.ClearSession();
            return RedirectToAction("Index", "Shop");
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword pass)
        {
            var user = SessionCustomer.GetUser();
            if(user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var dataUser = db.AdminUsers.Find(user.UserId);
            if (dataUser == null)
            {
                ViewBag.Error = "Đổi mật khẩu không thành công";
                return View(pass);
            }
            var passOld = Helper.ToMD5(pass.PasswordOld);
            if (!dataUser.Password_User.Trim().Equals(passOld.Trim()))
            {
                ViewBag.Error = "Mật khẩu cũ không chính xác";
                return View(pass);
            }
            if (!pass.PasswordNew.Equals(pass.ConfirmPassword))
            {
                ViewBag.Error = "Mật khẩu mới và mật khẩu xác nhận không khớp";
                return View(pass);
            }
            var passNew = Helper.ToMD5(pass.PasswordNew);
            dataUser.Password_User = passNew;
            db.Entry(dataUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Logout", "Login");
        }
    }
}