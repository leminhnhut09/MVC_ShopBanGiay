using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb_MVC_ShopGiay.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult TuVan()
        {
            return View();
        }

        //---------------------------------
        private List<GIAY> LayGIAYmoi(int count)
        {
            return db.GIAYs.OrderByDescending(a => a.NGAYCAPNHAT).Take(count).ToList();
        }
        public ActionResult Test1()
        {
            List<GIAY> lstGiay = db.GIAYs.OrderByDescending(a => a.NGAYCAPNHAT).Take(5).ToList();
            return View(lstGiay);
        }
    }
}
