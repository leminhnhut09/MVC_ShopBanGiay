﻿using System.Web;
using System.Web.Mvc;

namespace DoAnWeb_MVC_ShopGiay
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}