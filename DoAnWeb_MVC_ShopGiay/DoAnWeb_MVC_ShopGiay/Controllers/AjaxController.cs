using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb_MVC_ShopGiay.Models;

namespace DoAnWeb_MVC_ShopGiay.Controllers
{

    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult demoAjax()
        {
            List<LienHeAjax> lst = new List<LienHeAjax>();
            var lhs = db.LIENHE2s.ToList();
            foreach (var lh in lhs)
            {
                lst.Add(new LienHeAjax()
                {
                    MaLH = lh.MALH,
                    subject = lh.SUBJECT,
                    mess=lh.MESS
                });
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

    }
}
