using System;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Stormpath.Configuration.Abstractions.Immutable;
using Stormpath.Owin.Abstractions;
using Stormpath.Owin.Middleware;
using Stormpath.SDK.Account;
using Stormpath.SDK.Logging;

namespace Stormpath.AspNet
{
    public class RazorViewRenderer : IViewRenderer
    {
        private static readonly string HttpContextKey = "System.Web.HttpContextBase";
        private const string ControllerKey = "controller";

        private readonly ILogger _logger;

        public RazorViewRenderer(ILogger logger)
        {
            _logger = logger;
        }

        public Task<bool> RenderAsync(string name, object model, IOwinEnvironment context,
            CancellationToken cancellationToken)
        {
            var httpContext = context.Request[HttpContextKey] as HttpContextWrapper;
            if (httpContext == null)
            {
                _logger.Error($"Request did not include item '{HttpContextKey}'", nameof(RazorViewRenderer));
                return Task.FromResult(false);
            }

            // TODO ideally this would be done by the existing middleware pipeline
            // if authentication and view rendering were split up
            GetUserIdentity(httpContext, _logger);

            try
            {
                var controllerContext = CreateController<EmptyController>(httpContext, CreateRouteData()).ControllerContext;
                var view = FindView(name, model, controllerContext, _logger);

                using (var writer = new StreamWriter(context.Response.Body))
                {
                    var viewContext = new ViewContext(
                        controllerContext,
                        view,
                        controllerContext.Controller.ViewData,
                        controllerContext.Controller.TempData,
                        writer);

                    cancellationToken.ThrowIfCancellationRequested();
                    view.Render(viewContext, writer);

                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, null, nameof(RazorViewRenderer));
                return Task.FromResult(false);
            }
        }

        private static IView FindView(string path, object model, ControllerContext controllerContext, ILogger logger)
        {
            var viewEngineResult = ViewEngines.Engines.FindView(controllerContext, path, null);

            if (viewEngineResult?.View == null)
            {
                logger.Trace($"Could not find Razor view '{path}'", nameof(RazorViewRenderer));
                return null;
            }

            var view = viewEngineResult.View;
            controllerContext.Controller.ViewData.Model = model;

            return view;
        }

        private static void GetUserIdentity(HttpContextBase httpContext, ILogger logger)
        {
            var owinContext = httpContext.GetOwinContext();
            var config = owinContext.Get<StormpathConfiguration>(OwinKeys.StormpathConfiguration);
            var scheme = owinContext.Get<string>(OwinKeys.StormpathUserScheme);
            var account = owinContext.Get<IAccount>(OwinKeys.StormpathUser);

            var handler = new RouteProtector(config.Application, config.Web, null, null, null, null, logger);
            var isAuthenticatedRequest = handler.IsAuthenticated(scheme, scheme, account);

            if (isAuthenticatedRequest)
            {
                httpContext.User = AccountIdentityTransformer.CreatePrincipal(account, scheme);
            }

            if (httpContext.User == null)
            {
                httpContext.User = new GenericPrincipal(new GenericIdentity(""), new string[0]);
            }
        }

        private static T CreateController<T>(HttpContextWrapper httpContext, RouteData routeData)
            where T : Controller, new()
        {
            var controller = new T();

            controller.ControllerContext = new ControllerContext(httpContext, routeData, controller);
            return controller;
        }

        private static RouteData CreateRouteData()
        {
            var routeData = new RouteData();
            routeData.Values.Add(ControllerKey, "Stormpath");
            return routeData;
        }

    }

    public class EmptyController : Controller
    {
    }
}
