using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb_MVC_ShopGiay.Controllers
{
    public class PanerController : Controller
    {
        //
        // GET: /Paner/
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pener()
        {
            List<PANER> lstPN = db.PANERs.ToList();
            return View(lstPN);
        }
    }
}
