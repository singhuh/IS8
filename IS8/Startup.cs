using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IS8.Startup))]
namespace IS8
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
