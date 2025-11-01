// App_Start/WebApiConfig.cs
using System.Web.Http;
using Newtonsoft.Json;

public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // فقط JSON
        config.Formatters.Remove(config.Formatters.XmlFormatter);
        config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
            ReferenceLoopHandling.Ignore;

        config.MapHttpAttributeRoutes();

        // مسیر پیش‌فرض api
        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
    }
}
