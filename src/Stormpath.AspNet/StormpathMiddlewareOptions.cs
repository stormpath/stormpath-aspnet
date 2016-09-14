using Stormpath.SDK.Cache;
using Stormpath.SDK.Logging;

namespace Stormpath.AspNet
{
    public sealed class StormpathMiddlewareOptions
    {
        public object Configuration { get; set; }

        public ILogger Logger { get; set; }

        public ICacheProviderBuilder CacheProvider { get; set; }
    }
}
