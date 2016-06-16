using System;
using System.Threading;
using System.Threading.Tasks;
using Stormpath.Owin.Abstractions;

namespace Stormpath.AspNet
{
    public class RazorViewRenderer : IViewRenderer
    {
        public Task<bool> RenderAsync(string name, object model, IOwinEnvironment context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(); // todo
        }
    }
}
