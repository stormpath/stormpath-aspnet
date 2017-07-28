using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet
{
    public sealed class StormpathMiddlewareOptions
    {
        public object Configuration { get; set; }

        public ILogger Logger { get; set; } = NullLogger.Instance;

        public IDistributedCache CacheProvider { get; set; }

        public IDictionary<Type, DistributedCacheEntryOptions> CacheEntryOptions { get; set; }

        public Func<PreChangePasswordContext, CancellationToken, Task> PreChangePasswordHandler { get; set; }

        public Func<PostChangePasswordContext, CancellationToken, Task> PostChangePasswordHandler { get; set; }

        public Func<PreLoginContext, CancellationToken, Task> PreLoginHandler { get; set; }

        public Func<PostLoginContext, CancellationToken, Task> PostLoginHandler { get; set; }

        public Func<PreLogoutContext, CancellationToken, Task> PreLogoutHandler { get; set; }

        public Func<PostLogoutContext, CancellationToken, Task> PostLogoutHandler { get; set; }

        public Func<PreRegistrationContext, CancellationToken, Task> PreRegistrationHandler { get; set; }

        public Func<PostRegistrationContext, CancellationToken, Task> PostRegistrationHandler { get; set; }

        public Func<PreVerifyEmailContext, CancellationToken, Task> PreVerifyEmailHandler { get; set; }

        public Func<PostVerifyEmailContext, CancellationToken, Task> PostVerifyEmailHandler { get; set; }

        public Func<SendVerificationEmailContext, CancellationToken, Task> SendVerificationEmailHandler { get; set; }
    }
}
