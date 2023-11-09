using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_levent.Models;

namespace web_levent.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        LeventEntities _context = new LeventEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCategory(string search)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var data = _context.Categories.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.Name_Cate.ToLower().Contains(search.ToLower())).ToList();
            }
            int total = data.Count;
            return Json(new { rows = data, total = total }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateOrEdit(Category category)
        {
            try
            {
                if (string.IsNullOrEmpty(category.Name_Cate))
                {
                    return Json(new { status = false, message = "Vui lòng nhập tên danh mục" }, JsonRequestBehavior.AllowGet);
                }
                var ckName = _context.Categories.FirstOrDefault(x => x.Name_Cate.ToLower().Equals(category.Name_Cate.ToLower().Trim()) && x.ID_Cate != category.ID_Cate);
                if(ckName != null)
                {
                    return Json(new { status = false, message = "Tên danh mục đã tồn tại" }, JsonRequestBehavior.AllowGet);
                }
                if(category.ID_Cate == 0)
                {
                    _context.Categories.Add(category);
                }
                else
                {
                    var entity = _context.Categories.Find(category.ID_Cate);
                    if(entity == null)
                    {
                        return Json(new { status = false, message = "Cập nhật không thành công" }, JsonRequestBehavior.AllowGet);
                    }
                    entity.Name_Cate = category.Name_Cate;
                    _context.Entry(entity).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return Json(new { status = true, message = "Cập nhật thông tin thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Có lỗi xảy ra. Vui lòng thử lại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Delete(int Id)
        {
            try
            {
                var product = _context.Products.Where(x => x.ID_Cate == Id).ToList();
                if(product != null && product.Count > 0)
                {
                    return Json(new { status = false, message = "Không được phép xóa vì danh mục đang có sản phẩm" }, JsonRequestBehavior.AllowGet);
                }
                var entity = _context.Categories.Find(Id);
                if(entity == null)
                {
                    return Json(new { status = false, message = "Xóa không thành công" }, JsonRequestBehavior.AllowGet);
                }
                _context.Categories.Remove(entity);
                _context.SaveChanges();
                return Json(new { status = true, message = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Có lỗi xảy ra. Vui lòng thử lại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}