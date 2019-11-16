using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AuctionsSystem.Startup))]
namespace AuctionsSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
