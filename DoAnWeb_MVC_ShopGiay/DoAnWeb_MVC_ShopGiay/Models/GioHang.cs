using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb_MVC_ShopGiay.Models
{
    
    public class GioHang
    {
        dbQL_GIAYDataContext db = new dbQL_GIAYDataContext();
        public string MaGiay { set; get; }
        public string TenGiay { set; get; }
        public string AnhBia { set; get; }
        public double DonGia { set; get; }
        public int SoLuong { set; get; }
        public double ThanhTien
        {
            get { return SoLuong * DonGia; }
        }

        // Khởi tạo giỏ hàng
        public GioHang(string MAGIAY)
        {
            MaGiay = MAGIAY;
            GIAY giay = db.GIAYs.Single(s => s.MAGIAY == MaGiay);
            TenGiay = giay.TENGIAY;
            AnhBia = giay.ANHBIA;
            DonGia = double.Parse(giay.GIABAN.ToString());
            SoLuong = 1;

        }
    }
}