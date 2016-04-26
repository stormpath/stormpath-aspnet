using System;
using System.Threading;
using System.Threading.Tasks;
using Stormpath.Owin.Abstractions;

namespace Stormpath.AspNet
{
    // TODO hack
    public class SimpleViewRenderer : IViewRenderer
    {
        public Task RenderAsync(string viewName, object viewModel, IOwinEnvironment context, CancellationToken cancellationToken)
        {
            var view = Stormpath.Owin.Views.Precompiled.ViewResolver.GetView(viewName);
            if (view == null)
            {
                // Or, hook into your existing view rendering implementation
                throw new Exception($"View '{viewName}' not found.");
            }

            return view.ExecuteAsync(viewModel, context.Response.Body);
        }
    }
}
