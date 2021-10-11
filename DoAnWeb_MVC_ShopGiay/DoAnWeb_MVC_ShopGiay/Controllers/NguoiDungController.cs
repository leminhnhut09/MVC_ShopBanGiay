using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb_MVC_ShopGiay.Controllers
{
    public class NguoiDungController : Controller
    {
        //
        // GET: /NguoiDung/
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        public ActionResult Index() 
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            //Session["tentaikhoan"] = null;
            //Session["taikhoan"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KHACHHANG kh, FormCollection f)
        {
            var hoten = f["HotenKH"];
            var tendn = f["TenDN"];
            var matkhau = f["MatKhau"];
            var rematkhau = f["ReMatKhau"];
            var gioitinh = f["GioiTinh"];
            var dienthoai = f["DienThoai"];
            var ngaysinh = String.Format("{0:MM/DD/YYYY}", f["NgaySinh"]);
            var email = f["Email"];
            var diachi = f["DiaChi"];
            var quyen = "User";

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Tên đăng nhập không được để trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Mật khẩu không được để trống";
            }
            if (String.IsNullOrEmpty(rematkhau))
            {
                ViewData["Loi4"] = "Mật khẩu không trùng khớp";
            }
            if (String.IsNullOrEmpty(gioitinh))
            {
                ViewData["Loi5"] = "Vui lòng chọn giới tính";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Vui lòng nhập số điện thoại";
            }
            if (String.IsNullOrEmpty(ngaysinh))
            {
                ViewData["Loi7"] = "Vui lòng nhập ngày sinh";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi8"] = "Vui lòng nhập email";
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi9"] = "Vui lòng nhập địa chỉ";
            }
            if(!matkhau.Equals(rematkhau))
            {
                @ViewData["loi_khongkhop"] = "Mật khẩu không khớp";
            }
           
            // KIỂM TRA TRÙNG

            List<KHACHHANG> check = db.KHACHHANGs.Where(k => k.TAIKHOAN == tendn).ToList();

            if (check.Count() != 0)
            {
                ViewData["Loi10"] = "Tài khoản đã có người đăng ký !";
                return View();
            }

            if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(tendn) && !String.IsNullOrEmpty(matkhau) 
                && !String.IsNullOrEmpty(ngaysinh) && !String.IsNullOrEmpty(diachi)
                && !String.IsNullOrEmpty(gioitinh) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(dienthoai) && matkhau.Equals(rematkhau))
            {
                kh.HOTEN = hoten;
                kh.TAIKHOAN = tendn;
                kh.MATKHAU = matkhau;
                kh.NGAYSINH = DateTime.Parse(ngaysinh);
                kh.DIACHI = diachi;
                kh.GIOITINH = gioitinh;
                kh.QUYEN = quyen;
                kh.EMAIL = email;
                kh.SDT =dienthoai ;

                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("DangNhap", "NguoiDung");
            }

            return View();

        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            var tendn = f["TenDN"];
            var matkhau = f["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi_dn_1"] = "Tên đăng nhập không được bỏ trống!";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi_dn_2"] = "Vui lòng nhập mật khẩu";
            }
            if (!String.IsNullOrEmpty(tendn) && !String.IsNullOrEmpty(matkhau))
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(k => k.TAIKHOAN.Equals(tendn) && k.MATKHAU.Equals(matkhau));
                if (kh != null)
                {
                    Session["tentaikhoan"] = kh.TAIKHOAN;
                    Session["taikhoan"] = kh;
                    return RedirectToAction("GiayPartial", "Giay");
                }
                else
                    ViewBag.TB = "Sai tên đăng nhập hoặc mật khẩu, vui lòng nhập lại!";
            }

            return View();
        }

        [HttpGet]
        public ActionResult DangXuat()
        {
            Session["tentaikhoan"] = null;
            Session["taikhoan"] = null;
            return View();
        }
    }

    
}
