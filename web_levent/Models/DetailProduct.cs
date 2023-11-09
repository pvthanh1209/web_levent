using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_levent.Models
{
    public class DetailProduct
    {
        public int ID_Detail { get; set; }
        public Nullable<int> ID_Pro { get; set; }
        public string Name_Pro { get; set; }
        public Nullable<double> Price_Pro { get; set; }
        public string Img_Pro { get; set; }
        public Nullable<int> Quantity_Pro { get; set; }
        public string ColorPr_Name { get; set; }
        public string Img_Pro_Color { get; set; }
        public string Size_Name { get; set; }
        public int ID_Cate { get; set; }
        public HttpPostedFileBase fileUpLoad { get; set; }
        public HttpPostedFileBase fileUpLoadColor { get; set; }
    }
}