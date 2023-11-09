using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_levent.Models;

namespace web_levent.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        LeventEntities db = new LeventEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetOrder(string search)
        {
            var data = db.Orders.Join(db.AdminUsers,
                              o => o.UserId,
                              au => au.ID_User,
                              (o, au) => new
                              {
                                  Id = o.OrderId,
                                  OrderCode = o.OrderCode,
                                  FullName = au.Full_Name,
                                  Phone = au.Phone_Number,
                                  Address = au.Address,
                                  TotalPrice = o.TotalPrice,
                                  Status = o.Status,
                                  CreateDate = o.CreatedDate
                              });
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.OrderCode.ToLower().Contains(search.ToLower().Trim()));
            }
            int total = data.Count();
            return Json(new { rows = data, total = total }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrderDetail(int orderId)
        {
            ViewBag.OrderId = orderId;
            var order = db.Orders.Find(orderId);
            if(order != null)
            {
                ViewBag.OrderCode = order.OrderCode;
            }
            return View();
        }
        public JsonResult GetOrderDetail(int orderId)
        {
            var data = db.OrderDetails
                 .Join(db.Products, od => od.ProductId, p => p.ID_Pro, (od, p) => new { od, p })
                 .Join(db.Details, odd => odd.od.DetailId, d => d.ID_Detail, (odd, d) => new { odd, d})
                 .Join(db.Color_De, dd => dd.d.ID_Detail, cd => cd.ID_Details, (dd, cd) => new { dd, cd})
                 .Join(db.Size_De, dt => dt.dd.d.ID_Detail, sd => sd.ID_Details, (dt, sd) => new {dt, sd})
                 .Select(m => new
                 {
                     Id = m.dt.dd.odd.od.Order_Detail_Id,
                     OrderId = m.dt.dd.odd.od.OrderId,
                     ProductName = m.dt.dd.odd.p.Name_Pro,
                     ColorName = m.dt.cd.ColorPr_Name,
                     SizeName = m.sd.Size_Name,
                     Quantity = m.dt.dd.odd.od.Quantity,
                     Price = m.dt.dd.odd.od.Price
                 }).Where(x => x.OrderId == orderId).ToList();
            decimal totalPrice = data.Sum(x => x.Price.Value);
            int total = data.Count;
            return Json(new { rows = data, total = total, totalPrice = totalPrice }, JsonRequestBehavior.AllowGet);
        }
    }
}