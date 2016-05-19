using System.Collections.Generic;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_Register
    {
        public void Configuation_RegisterUri(IAppBuilder app)
        {
            #region code/registration/aspnet/configure_uri.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Register = new WebRegisterRouteConfiguration
                    {
                        Uri = "/createAccount"
                    }
                }
            });
            #endregion
        }

        public void Configuration_RegisterFormFieldsRequired(IAppBuilder app)
        {
            #region code/registration/aspnet/configure_form_fields_required.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Register = new WebRegisterRouteConfiguration
                    {
                        Form = new WebRegisterRouteFormConfiguration
                        {
                            Fields = new Dictionary<string, WebFieldConfiguration>
                            {
                                ["givenName"] = new WebFieldConfiguration { Required = false },
                                ["surname"] = new WebFieldConfiguration { Required = false }
                            }
                        }
                    }
                }
            });
            #endregion
        }

        public void Configuration_RegisterCustomFormField(IAppBuilder app)
        {
            #region code/registration/aspnet/configure_custom_form_field.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Register = new WebRegisterRouteConfiguration
                    {
                        Form = new WebRegisterRouteFormConfiguration
                        {
                            Fields = new Dictionary<string, WebFieldConfiguration>
                            {
                                ["favoriteColor"] = new WebFieldConfiguration
                                {
                                    Enabled = true,
                                    Visible = true,
                                    Label = "Favorite Color",
                                    Placeholder = "e.g. red, blue",
                                    Required = true,
                                    Type = "text"
                                }
                            }
                        }
                    }
                }
            });
            #endregion
        }

        public void Configuration_RegisterFieldOrder(IAppBuilder app)
        {
            #region code/registration/aspnet/configure_field_order.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Register = new WebRegisterRouteConfiguration
                    {
                        Form = new WebRegisterRouteFormConfiguration
                        {
                            FieldOrder = new string[]
                            {
                                "surname",
                                "givenName",
                                "email",
                                "password"
                            }
                        }
                    }
                }
            });
            #endregion
        }
    }
}
