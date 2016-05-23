using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassGeneralsModels.Startup))]
namespace PassGeneralsModels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
