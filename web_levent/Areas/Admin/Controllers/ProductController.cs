using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using web_levent.Models;

namespace web_levent.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        LeventEntities db = new LeventEntities();
        public ActionResult Index()
        {
            ViewBag.ListCategory = db.Categories.ToList();
            return View();
        }
        public JsonResult GetProduct(string search)
        {
            var data = db.Products.Join(db.Categories,
                                p => p.ID_Cate,
                                c => c.ID_Cate,
                                (p, c) => new
                                {
                                    Id = p.ID_Pro,
                                    ProductName = p.Name_Pro,
                                    ImageProduct = p.Img_pro,
                                    PriceProduct = p.Price_Pro,
                                    CateId = p.ID_Cate,
                                    CateName = c.Name_Cate
                                });
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                data = data.Where(x => x.ProductName.ToLower().Contains(search) || x.CateName.ToLower().Contains(search));
            }
            int total = data.Count();
            return Json(new { rows = data, total = total }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateOrEdit(Product product, HttpPostedFileBase fileUpLoad)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Name_Pro))
                {
                    return Json(new { status = false, message = "Vui lòng nhập tên sản phẩm" }, JsonRequestBehavior.AllowGet);
                }
                if (product.ID_Cate == 0)
                {
                    return Json(new { status = false, message = "Vui lòng chọn danh mục" }, JsonRequestBehavior.AllowGet);
                }
                string pathDb = "";
                if (fileUpLoad != null)
                {
                    Regex rgx = new Regex(@"(.*?)\.(jpg|bmp|png|gif|JPG|PNG|BMP|GIF|docx|docs|doc|xls|xlsx|pdf)$");
                    if (!rgx.IsMatch(fileUpLoad.FileName))
                    {
                        return Json(new { status = false, message = "Định dạng ảnh không hợp lệ" }, JsonRequestBehavior.AllowGet);
                    }
                    string pic = DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(fileUpLoad.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Product/"), pic);
                    fileUpLoad.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fileUpLoad.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    pathDb = "/Uploads/Product/" + pic;
                }
                var ckNameProduct = db.Products.FirstOrDefault(x => x.Name_Pro.ToLower().Equals(product.Name_Pro.ToLower()) && x.ID_Pro != product.ID_Pro);
                if (ckNameProduct != null)
                {
                    return Json(new { status = false, message = "Tên sản phẩm đã tồn tại" }, JsonRequestBehavior.AllowGet);
                }
                if (product.ID_Pro == 0)
                {
                    product.Img_pro = pathDb;
                    db.Products.Add(product);
                }
                else
                {
                    var entity = db.Products.Find(product.ID_Pro);
                    if (entity == null)
                    {
                        return Json(new { status = false, message = "Chỉnh sửa không thành công" }, JsonRequestBehavior.AllowGet);
                    }
                    entity.ID_Cate = product.ID_Cate;
                    entity.Name_Pro = product.Name_Pro;
                    entity.Img_pro = (!string.IsNullOrEmpty(pathDb) ? pathDb : entity.Img_pro);
                    entity.Price_Pro = product.Price_Pro;
                    db.Entry(entity).State = EntityState.Modified;
                }
                db.SaveChanges();
                return Json(new { status = true, message = "Cập nhật thông tin thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = false, message = "Có lỗi xảy ra. Vui lòng thử lại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Delete(int Id)
        {
            try
            {
                var detail = db.Details.Where(x => x.ID_Pro == Id).ToList();
                if(detail != null && detail.Count > 0)
                {
                    return Json(new { status = false, message = "Không xóa được sản phẩm vì còn tồn tại chi tiết sản phẩm đó" }, JsonRequestBehavior.AllowGet);
                }
                var entity = db.Products.Find(Id);
                if (entity == null)
                {
                    return Json(new { status = false, message = "Xóa không thành công" }, JsonRequestBehavior.AllowGet);
                }
                db.Products.Remove(entity);
                db.SaveChanges();
                return Json(new { status = true, message = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Có lỗi xảy ra. Vui lòng thử lại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Detail(int Id)
        {
            ViewBag.ListColor = db.Color_De.ToList();
            ViewBag.ListSize = db.Size_De.ToList();
            var model = db.Products.Find(Id);
            return View(model);
        }

        public JsonResult GetProductDetail(int productId, string search)
        {
            var data = db.Details
                .Join(db.Color_De, d => d.ID_Detail, cd => cd.ID_Details, (d, cd) => new { d, cd })
                .Join(db.Size_De, dd => dd.d.ID_Detail, sd => sd.ID_Details, (dd, sd) => new { dd, sd })
                .Select(m => new
                {
                    Id = m.dd.d.ID_Detail,
                    ColorId = m.dd.cd.IdColor,
                    ProductId = m.dd.d.ID_Pro,
                    NamePro = m.dd.d.Name_Pro,
                    PricePro = m.dd.d.Price_Pro,
                    ColorName = m.dd.cd.ColorPr_Name,
                    ImgPro = m.dd.d.Img_Pro,
                    ImgColor = m.dd.cd.Img_Pro,
                    SizeName = m.sd.Size_Name,
                    QuantityPro = m.dd.d.Quantity_Pro
                }).Where(x => x.ProductId == productId).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                var key = search.ToLower().Trim();
                data = data.Where(x => x.NamePro.Contains(key) || x.ColorName.Contains(key) || x.SizeName.Contains(key)).ToList();
            }
            var total = data.Count;
            return Json(new { rows = data, total = total }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateDetail(DetailProduct detail)
        {
            try
            {
                if (string.IsNullOrEmpty(detail.Name_Pro))
                {
                    return Json(new { status = false, message = "Vui lòng nhập tên sản phẩm" }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(detail.ColorPr_Name))
                {
                    return Json(new { status = false, message = "Vui lòng nhâp màu sắc" }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(detail.Size_Name))
                {
                    return Json(new { status = false, message = "Vui lòng nhâp size" }, JsonRequestBehavior.AllowGet);
                }
                string pathPro = string.Empty;
                string pathProDetail = string.Empty;
                if (detail.fileUpLoad != null)
                {
                    Regex rgx = new Regex(@"(.*?)\.(jpg|bmp|png|gif|JPG|PNG|BMP|GIF|docx|docs|doc|xls|xlsx|pdf)$");
                    if (!rgx.IsMatch(detail.fileUpLoad.FileName))
                    {
                        return Json(new { status = false, message = "Định dạng ảnh không hợp lệ" }, JsonRequestBehavior.AllowGet);
                    }
                    string pic = "dt" + DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(detail.fileUpLoad.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Product/"), pic);
                    detail.fileUpLoad.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        detail.fileUpLoad.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    pathPro = "/Uploads/Product/" + pic;
                }
                if (detail.fileUpLoadColor != null)
                {
                    Regex rgx = new Regex(@"(.*?)\.(jpg|bmp|png|gif|JPG|PNG|BMP|GIF|docx|docs|doc|xls|xlsx|pdf)$");
                    if (!rgx.IsMatch(detail.fileUpLoadColor.FileName))
                    {
                        return Json(new { status = false, message = "Định dạng ảnh không hợp lệ" }, JsonRequestBehavior.AllowGet);
                    }
                    string pic = "color" + DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(detail.fileUpLoadColor.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Product/"), pic);
                    detail.fileUpLoadColor.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        detail.fileUpLoadColor.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    pathProDetail = "/Uploads/Product/" + pic;
                }

                if (detail.ID_Detail == 0)
                {
                    var data = new Detail();
                    data.ID_Detail = 0;
                    data.ID_Pro = detail.ID_Pro;
                    data.Name_Pro = detail.Name_Pro;
                    data.Price_Pro = detail.Price_Pro;
                    data.Img_Pro = pathPro;
                    data.Quantity_Pro = detail.Quantity_Pro;
                    var myObject = db.Details.Add(data);
                    db.SaveChanges();
                    int idDetail = myObject.ID_Detail;
                    //insert color
                    var color = new Color_De();
                    color.IdColor = 0;
                    color.ID_Details = idDetail;
                    color.ColorPr_Name = detail.ColorPr_Name;
                    color.Img_Pro = pathProDetail;
                    db.Color_De.Add(color);
                    db.SaveChanges();
                    //Insert size
                    var size = new Size_De();
                    size.ID_Details = idDetail;
                    size.Size_Pro = 0;
                    size.ID_Cate = detail.ID_Cate;
                    size.Size_Name = detail.Size_Name;
                    db.Size_De.Add(size);
                    db.SaveChanges();
                }
                else
                {
                    var data = db.Details.Find(detail.ID_Detail);
                    data.Name_Pro = detail.Name_Pro;
                    data.Price_Pro = detail.Price_Pro;
                    data.Img_Pro = (!string.IsNullOrEmpty(pathPro) ? pathPro : data.Img_Pro);
                    data.Quantity_Pro = detail.Quantity_Pro;
                    db.Entry(data).State = EntityState.Modified;

                    var color = db.Color_De.Where(x => x.ID_Details == detail.ID_Detail).FirstOrDefault();
                    if(color != null)
                    {
                        color.ColorPr_Name = detail.ColorPr_Name;
                        color.Img_Pro = (!string.IsNullOrEmpty(pathProDetail) ? pathProDetail : color.Img_Pro);
                        db.Entry(color).State = EntityState.Modified;
                    }
                    var size = db.Size_De.Where(x => x.ID_Details == detail.ID_Detail).FirstOrDefault();
                    if(size != null)
                    {
                        size.ID_Cate = detail.ID_Cate;
                        size.Size_Name = detail.Size_Name;
                        db.Entry(size).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
                return Json(new { status = true, message = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { status = false, message = "Có lỗi xảy ra. Vui lòng thử lại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteDetail(int Id)
        {
            try
            {
                var color = db.Color_De.Where(x => x.ID_Details == Id).ToList();
                var size = db.Size_De.Where(x => x.ID_Details == Id).ToList();
                if((color != null && color.Count > 0) || (size != null && size.Count > 0))
                {
                    return Json(new { status = false, message = "Không thể xóa. Vì chi tiết sản phẩm đang tồn tại thuộc tính màu sắc và size của chi tiết sản phẩm" }, JsonRequestBehavior.AllowGet);
                }
                var entity = db.Details.Find(Id);
                if (entity == null)
                {
                    return Json(new { status = false, message = "Xóa không thành công" }, JsonRequestBehavior.AllowGet);
                }
                db.Details.Remove(entity);
                db.SaveChanges();
                return Json(new { status = true, message = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Có lỗi xảy ra. Vui lòng thử lại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Color_Product(int Id)
        {
            var data = db.Color_De.Find(Id);
            return Json(new { data = data});
        }
    }
}