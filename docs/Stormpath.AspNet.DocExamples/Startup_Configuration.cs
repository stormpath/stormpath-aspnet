using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{

    public class Startup_Configuration
    {
        public void Configuration_Model(IAppBuilder app)
        {
            #region code/configuration/aspnet/inline_config.cs
            app.UseStormpath(new StormpathConfiguration()
            {
                Application = new ApplicationConfiguration()
                {
                    Name = "My Application"
                },
                Web = new WebConfiguration()
                {
                    Register = new WebRegisterRouteConfiguration()
                    {
                        Enabled = false
                    }
                }
            });
            #endregion
        }

        public void Configuration_AnonymousModel(IAppBuilder app)
        {
            #region code/configuration/aspnet/anonymous_inline_config.cs
            app.UseStormpath(new
            {
                application = new
                {
                    name = "My Application"
                },
                web = new
                {
                    register = new
                    {
                        enabled = false
                    }
                }
            });
            #endregion
        }

        public void Configuration_DisableDefaultFeatures(IAppBuilder app)
        {
            #region code/configuration/aspnet/disable_default_features.cs
            app.UseStormpath(new StormpathConfiguration()
            {
                Web = new WebConfiguration()
                {
                    ForgotPassword = new WebForgotPasswordRouteConfiguration()
                    {
                        Enabled = false
                    },
                    ChangePassword = new WebChangePasswordRouteConfiguration()
                    {
                        Enabled = false
                    },
                    Login = new WebLoginRouteConfiguration()
                    {
                        Enabled = false
                    },
                    Logout = new WebLogoutRouteConfiguration()
                    {
                        Enabled = false
                    },
                    Me = new WebMeRouteConfiguration()
                    {
                        Enabled = false
                    },
                    Oauth2 = new WebOauth2RouteConfiguration()
                    {
                        Enabled = false
                    },
                    Register = new WebRegisterRouteConfiguration()
                    {
                        Enabled = false
                    },
                    VerifyEmail = new WebVerifyEmailRouteConfiguration()
                    {
                        Enabled = false
                    }
                }
            });
            #endregion
        }

        public void Configuration_ApiCredentials(IAppBuilder app)
        {
            #region code/configuration/aspnet/api_credentials.cs
            app.UseStormpath(new StormpathConfiguration()
            {
                Client = new ClientConfiguration()
                {
                    ApiKey = new ClientApiKeyConfiguration()
                    {
                        Id = "YOUR_API_KEY_ID",
                        Secret = "YOUR_API_KEY_SECRET"
                    }
                }
            });
            #endregion
        }

        public void Configuration_DisableHtml(IAppBuilder app)
        {
            #region code/configuration/aspnet/disable_html_produces.cs
            app.UseStormpath(new StormpathConfiguration()
            {
                Web = new WebConfiguration()
                {
                    Produces = new string[] { "application/json" }
                }
            });
            #endregion
        }

        public void Configuration_ServerUri(IAppBuilder app)
        {
            #region code/configuration/aspnet/server_uri.cs
            app.UseStormpath(new StormpathConfiguration()
            {
                Web = new WebConfiguration()
                {
                    ServerUri = "http://localhost:5000"
                }
            });
            #endregion
        }

        public void Configuration_BaseUrl(IAppBuilder app)
        {
            #region code/configuration/aspnet/stormpath_baseurl.cs
            app.UseStormpath(new StormpathConfiguration()
            {
                Client = new ClientConfiguration()
                {
                    BaseUrl = "https://enterprise.stormpath.io/v1"
                }
            });
            #endregion
        }
    }
}
