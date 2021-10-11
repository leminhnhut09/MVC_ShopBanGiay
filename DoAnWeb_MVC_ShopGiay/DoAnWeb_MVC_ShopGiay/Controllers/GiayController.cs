using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb_MVC_ShopGiay.Controllers
{
    public class GiayController : Controller
    {
        //
        // GET: /Giay/

        public ActionResult Index()
        {
            return View();
        }
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        public ActionResult GiayPartial()
        {

            var ListSach = db.GIAYs.OrderBy(s => s.TENGIAY).ToList();
            return View(ListSach);
        }
        public ActionResult XemChiTiet(string mg)
        {
            GIAY giay = db.GIAYs.SingleOrDefault(s => s.MAGIAY.Equals(mg));
            if (giay == null)
            {
                return HttpNotFound();
            }
            return View(giay);
        }

        public ActionResult LoaiGiay()
        {
            var ListLoaiGiay = db.LOAIGIAYs.OrderBy(cd => cd.TENLOAI).ToList();

            return View(ListLoaiGiay);
        }
        public ActionResult GiayTheoLoai(string maLoai)
        {
            var lstGiay = db.GIAYs.OrderBy(s => s.TENGIAY).Where(s => s.MALOAI == maLoai).ToList();
            if (lstGiay.Count == 0)
            {
                ViewBag.Giay = "Không có giày loại này !";
            }
            return View(lstGiay);
        }
        public ActionResult TimGiay(FormCollection f)
        {
            string tengiay = f["TenGiay"];
            var lstGiay = db.GIAYs.OrderBy(s => s.TENGIAY).Where(k => k.TENGIAY.Contains(tengiay)).ToList();
            if (string.IsNullOrEmpty(tengiay))
                lstGiay = new List<GIAY>();
            if (lstGiay.Count == 0)
            {
                ViewBag.Sach = "Không có giày này !";
            }
            return View(lstGiay);
        }
    }
}
