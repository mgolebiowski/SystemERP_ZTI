using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERP_ZTI.Startup))]
namespace ERP_ZTI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
