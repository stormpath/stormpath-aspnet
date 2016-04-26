using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace Stormpath.AspNet.TckHarness
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseStormpath(new
            {
                application = new
                {
                    name = "My Application"
                }
            });
        }
    }
}
