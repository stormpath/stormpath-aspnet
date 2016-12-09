using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_IdSite
    {
        public void Configuration_IdSite(IAppBuilder app)
        {
            #region code/id_site/aspnet/enable_idsite.cs

            app.UseStormpath(new StormpathConfiguration()
            {
                Web = new WebConfiguration()
                {
                    IdSite = new WebIdSiteConfiguration()
                    {
                        Enabled = true
                    }
                }
            });

            #endregion
        }
    }
}