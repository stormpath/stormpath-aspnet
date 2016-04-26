using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stormpath.AspNet
{
    public class StormpathAuthenticationOptions : Microsoft.Owin.Security.AuthenticationOptions
    {
        public StormpathAuthenticationOptions(string authenticationType)
            : base(authenticationType)
        {
            this.AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active;
        }
    }
}
