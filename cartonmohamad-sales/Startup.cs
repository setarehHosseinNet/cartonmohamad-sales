using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cartonmohamad_sales.Startup))]
namespace cartonmohamad_sales
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
