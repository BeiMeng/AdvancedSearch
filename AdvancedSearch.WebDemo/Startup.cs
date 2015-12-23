using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdvancedSearch.WebDemo.Startup))]
namespace AdvancedSearch.WebDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
