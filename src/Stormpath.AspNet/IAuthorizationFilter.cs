using Stormpath.SDK.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stormpath.AspNet
{
    public interface IAuthorizationFilter
    {
        Task<bool> IsAuthorized(IAccount account);
    }
}
