using Microsoft.Owin;
using OsCoreApplication.Logger;
using Owin;

[assembly: OwinStartup(typeof(OsCoreApplication.Startup))]
namespace OsCoreApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            OsLog.Instance();
        }
    }
}