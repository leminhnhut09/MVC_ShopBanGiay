using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb_MVC_ShopGiay.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        //
        // GET: /QuanLySanPham/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SanPhamAll()
        {
            List<GIAY> lstGiay = db.GIAYs.OrderByDescending(k => k.MAGIAY).ToList();
            return View(lstGiay);
        }
        // xóa
        public ActionResult DeleteGiay(string ma)
        {
            //lay ra giay can xoa
            GIAY giay = db.GIAYs.Where(d => d.MAGIAY.Equals(ma)).FirstOrDefault();
            if (giay == null)
            {
                return HttpNotFound();
            }
            db.GIAYs.DeleteOnSubmit(giay);
            db.SubmitChanges();
            return RedirectToAction("SanPhamAll", "QuanLySanPham");
        }
        // thêm
        public ActionResult ThemGiay(GIAY giay, FormCollection f)
        {
            var magiay = f["MaGiay"];
            var tengiay = f["TenGiay"];
            var giaban = String.Format("{0:0,0}", f["GiaBan"]);
            var mota = f["MoTa"];
            var anhbia = f["AnhBia"];
            var ngaycapnhat = String.Format("{0:MM/DD/YYYY}", f["NgayCapNhat"]);
            var soluongton = String.Format("{0:0,0}", f["SoLuongTon"]);
            var maloai = f["MaLoai"];
            var mansx = f["MaNSX"];


            if (String.IsNullOrEmpty(magiay))
            {
                ViewData["Loi1"] = "Mã giày không được để trống";
            }
            //if (String.IsNullOrEmpty(tendn))
            //{
            //    ViewData["Loi2"] = "Tên đăng nhập không được để trống";
            //}
            //if (String.IsNullOrEmpty(matkhau))
            //{
            //    ViewData["Loi3"] = "Mật khẩu không được để trống";
            //}
            //if (String.IsNullOrEmpty(rematkhau))
            //{
            //    ViewData["Loi4"] = "Mật khẩu không trùng khớp";
            //}
            //if (String.IsNullOrEmpty(gioitinh))
            //{
            //    ViewData["Loi5"] = "Vui lòng chọn giới tính";
            //}
            //if (String.IsNullOrEmpty(dienthoai))
            //{
            //    ViewData["Loi6"] = "Vui lòng nhập số điện thoại";
            //}
            //if (String.IsNullOrEmpty(ngaysinh))
            //{
            //    ViewData["Loi7"] = "Vui lòng nhập ngày sinh";
            //}
            //if (String.IsNullOrEmpty(email))
            //{
            //    ViewData["Loi8"] = "Vui lòng nhập email";
            //}
            //if (String.IsNullOrEmpty(diachi))
            //{
            //    ViewData["Loi9"] = "Vui lòng nhập địa chỉ";
            //}
            // KIỂM TRA TRÙNG

            //List<KHACHHANG> check = db.KHACHHANGs.Where(k => k.TAIKHOAN == tendn).ToList();

            //if (check.Count() != 0)
            //{
            //    ViewData["Loi10"] = "Tài khoản đã có người đăng ký !";
            //    return View();
            //}

            //if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(tendn) && !String.IsNullOrEmpty(matkhau)
            //    && !String.IsNullOrEmpty(ngaysinh) && !String.IsNullOrEmpty(diachi)
            //    && !String.IsNullOrEmpty(gioitinh) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(dienthoai))

            if (!String.IsNullOrEmpty(tengiay) && !String.IsNullOrEmpty(magiay))
            {
                giay.MAGIAY = magiay;
                giay.TENGIAY = tengiay;
                giay.MOTA = mota;
                giay.ANHBIA = anhbia;
                giay.SOLUONGTON = int.Parse(soluongton);
                giay.GIABAN = float.Parse(giaban);
                giay.NGAYCAPNHAT = DateTime.Parse(ngaycapnhat);
                giay.MANSX = mansx;
                giay.MALOAI = maloai;

                db.GIAYs.InsertOnSubmit(giay);
                db.SubmitChanges();
            }

            return RedirectToAction("SanPhamAll", "QuanLySanPham");

        }
        public ActionResult SuaGiay(string ma)
        {
            GIAY giay = db.GIAYs.Where(d => d.MAGIAY.Equals(ma)).FirstOrDefault();
            if (giay == null)
            {
                return HttpNotFound();
            }
            return View(giay);
        }
        [HttpPost]
        public ActionResult SuaGiay(GIAY giay)
        {
            GIAY g = db.GIAYs.Where(d => d.MAGIAY.Equals(giay.MAGIAY)).FirstOrDefault();
            g.GIABAN = giay.GIABAN;
            g.TENGIAY = giay.TENGIAY;
            g.MOTA = giay.MOTA;
            g.ANHBIA = giay.ANHBIA;
            g.SOLUONGTON = giay.SOLUONGTON;
            g.NGAYCAPNHAT = giay.NGAYCAPNHAT;
            g.MANSX = giay.MANSX;
            g.MALOAI = giay.MALOAI;
            if (ModelState.IsValid)
            {
                //db.GIAYs.Attach(g);
                db.SubmitChanges();
                
                //db.SaveChanges();
                return RedirectToAction("SanPhamAll", "QuanLySanPham");
            }
            return RedirectToAction("SuaGiay", "QuanLySanPham");
        }
        //---------------------
        //public JsonResult List()
        //{
        //    List<GIAY> ds = db.GIAYs.ToList();

        //    return Json(ds, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult Add(GIAY f)
        //{
        //    int kq = 1;
        //    try
        //    {
        //        db.GIAYs.InsertOnSubmit(f);
        //        db.SubmitChanges();
        //    }
        //    catch
        //    {
        //        kq = 0;
        //    }
        //    return Json(kq, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetbyID(string ID)
        //{
        //    GIAY f = db.GIAYs.FirstOrDefault(x => x.MAGIAY == ID);
        //    return Json(f, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult Update(GIAY g)
        //{
        //    int kq = 1;
        //    try
        //    {
        //        GIAY giay = db.GIAYs.FirstOrDefault(m => m.MAGIAY == g.MAGIAY);
        //        //monan.name = f.name;
        //        //monan.type = f.type;
        //        //monan.price = f.price;
        //        giay.TENGIAY = g.TENGIAY;
        //        giay.MOTA = g.MOTA;
        //        giay.ANHBIA = g.ANHBIA;
        //        giay.GIABAN = g.GIABAN;
        //        giay.NGAYCAPNHAT = g.NGAYCAPNHAT;
        //        giay.SOLUONGTON = g.SOLUONGTON;
        //        giay.MALOAI = g.MALOAI;
        //        giay.MANSX = g.MANSX;

        //        UpdateModel(giay);
        //        db.SubmitChanges();
        //    }
        //    catch
        //    {
        //        kq = 0;
        //    }
        //    return Json(kq, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult Delete(string ID)
        //{
        //    List<GIAY> ds = db.GIAYs.ToList();
        //    var f = db.GIAYs.FirstOrDefault(x => x.MAGIAY == ID);
        //    db.GIAYs.DeleteOnSubmit(f);
        //    return Json(ds, JsonRequestBehavior.AllowGet);
        //}
    }
}
