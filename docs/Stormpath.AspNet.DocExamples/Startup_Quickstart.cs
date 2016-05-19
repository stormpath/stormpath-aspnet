#region code/quickstart/aspnet/configure.cs
using Microsoft.Owin;
using Owin;
using Stormpath.AspNet;

[assembly: OwinStartup(typeof(MyApplication.Startup))]
namespace MyApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStormpath();
        }
    }
}
#endregion