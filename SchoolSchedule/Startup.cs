using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolSchedule.Startup))]
namespace SchoolSchedule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
