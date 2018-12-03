using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PTPM_HTQLNH_DASHBOARD.Startup))]
namespace PTPM_HTQLNH_DASHBOARD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
