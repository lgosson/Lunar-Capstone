using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LunarLogic.Startup))]
namespace LunarLogic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
