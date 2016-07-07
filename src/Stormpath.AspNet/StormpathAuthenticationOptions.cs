namespace Stormpath.AspNet
{
    public class StormpathAuthenticationOptions : Microsoft.Owin.Security.AuthenticationOptions
    {
        public StormpathAuthenticationOptions()
            : base("Stormpath")
        {
            AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active;
        }

        public string[] AllowedAuthenticationSchemes { get; set; }
    }
}
