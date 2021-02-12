using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebForum.Startup))]
namespace WebForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
