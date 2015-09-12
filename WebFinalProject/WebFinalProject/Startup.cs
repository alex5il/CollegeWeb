using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebFinalProject.Startup))]
namespace WebFinalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
