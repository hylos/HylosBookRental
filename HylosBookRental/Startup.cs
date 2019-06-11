using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HylosBookRental.Startup))]
namespace HylosBookRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
