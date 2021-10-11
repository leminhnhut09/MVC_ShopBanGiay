using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb_MVC_ShopGiay.Controllers
{
    public class LienHeController : Controller
    {
        //
        // GET: /LienHe/
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LienHe(LIENHE2 lh, FormCollection f)
        {
            var hoten = f["HotenKH"];
            var email = f["Email"];
            var xacnhannguoi = f["Xacnhannguoi"];
            var subject = f["Subject"];
            var mess = f["Mess"];

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi2"] = "Email không được để trống";
            }
            if (String.IsNullOrEmpty(xacnhannguoi) || (2 != int.Parse(xacnhannguoi)))
            {
                ViewData["Loi3"] = "Mời nhập đúng";
            }
            if (String.IsNullOrEmpty(subject))
            {
                ViewData["Loi4"] = "Bạn muốn giúp gì?";
            }
            if (String.IsNullOrEmpty(mess))
            {
                ViewData["Loi5"] = "Vui lòng điền thông tin muốn giúp!!! Cảm Ơn";
            }
            if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(xacnhannguoi)
               && !String.IsNullOrEmpty(subject) && !String.IsNullOrEmpty(mess) && 2 == int.Parse(xacnhannguoi))
            {
                lh.HOTEN = hoten;
                lh.EMAIL = email;
                lh.XACNHANNGUOI = xacnhannguoi;
                lh.SUBJECT = subject;
                lh.MESS = mess;

                db.LIENHE2s.InsertOnSubmit(lh);
                db.SubmitChanges();
                return RedirectToAction("Index1", "LienHe");
            }


            return View();
        }

    }
}
