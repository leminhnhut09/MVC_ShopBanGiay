using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb_MVC_ShopGiay.Models;

namespace DoAnWeb_MVC_ShopGiay.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        public ActionResult Index()
        {
            return View();
        }
        // phương thức trả về giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        // add thêm vào giỏ hàng
        public ActionResult ThemGioHang(string ms, string strURL)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang Giay = lstGioHang.Find(sp => sp.MaGiay == ms);
            if (Giay == null)
            {
                Giay = new GioHang(ms);
                lstGioHang.Add(Giay);
                return Redirect(strURL);
            }
            else
            {
                Giay.SoLuong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;

            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(sp => sp.SoLuong);
            }
            return tsl;
        }

        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;

            if (lstGioHang != null)
            {
                ttt += lstGioHang.Sum(sp => sp.ThanhTien);
            }
            return ttt;
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index1", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return PartialView();
        }

        public ActionResult XoaGioHang(string MaSP)
        {
            List<GioHang> lstGioHang = LayGioHang();

            GioHang sp = lstGioHang.Single(s => s.MaGiay == MaSP);

            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.MaGiay == MaSP);
                return RedirectToAction("GioHang", "GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index1", "Home");
            }
            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult XoaGioHang_All()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index1", "Home");
        }

        public ActionResult CapNhatGioHang(string MaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.MaGiay == MaSP);
            if (sp != null)
            {
                sp.SoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang", "GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("GiayPartial", "Giay");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();

            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DONHANG ddh = new DONHANG();
            KHACHHANG kh = (KHACHHANG)Session["taikhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MAKH = kh.MAKH;
            ddh.NGAYDAT = DateTime.Now;
            var NgayGiao = String.Format("{0:dd/mm/yyyy}", f["NgayGiao"]);
            ddh.NGAYGIAO = DateTime.Parse(NgayGiao);
            ddh.DATHANHTOAN = "Chưa thanh toán";
            ddh.TINHTRANGGIAOHANG = 0;
            db.DONHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();

            foreach (var item in gh)
            {
                CHITIETDATHANG ctdh = new CHITIETDATHANG();
                ctdh.MADONHANG = ddh.MADONHANG;
                ctdh.MAGIAY = item.MaGiay;
                ctdh.SL = item.SoLuong;
                ctdh.DONGIA = (float)item.DonGia;
                db.CHITIETDATHANGs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;

            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }

    }
}
