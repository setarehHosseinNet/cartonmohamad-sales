using cartonmohamad_sales.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace cartonmohamad_sales
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());
        }
        protected void Application_BeginRequest()
        {
            // نمایش تاریخ/عدد به فارسی
            var fa = new CultureInfo("fa-IR");
            fa.DateTimeFormat.Calendar = new PersianCalendar();
            fa.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
            fa.NumberFormat.NumberDecimalSeparator = "/";
            Thread.CurrentThread.CurrentCulture = fa;
            Thread.CurrentThread.CurrentUICulture = fa;
        }
    }
}
