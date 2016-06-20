//// Adapted from https://github.com/RickStrahl/WestwindToolkit/blob/master/Westwind.Web.Mvc/Utils/ViewRenderer.cs
//// Licensed under MIT.

//using System;
//using System.Web;
//using System.Web.Mvc;
//using System.IO;
//using System.Web.Routing;

//namespace Stormpath.AspNet
//{
//    /// <summary>
//    /// Class that renders MVC views to a string using the
//    /// standard MVC View Engine to render the view. 
//    /// 
//    /// Requires that ASP.NET HttpContext is present to
//    /// work, but works outside of the context of MVC
//    /// </summary>
//    public class ViewRenderer
//    {
//        private const string ControllerKey = "controller";

//        /// <summary>
//        /// Required Controller Context
//        /// </summary>
//        protected ControllerContext Context { get; set; }

//        /// <summary>
//        /// Initializes the ViewRenderer with a Context.
//        /// </summary>
//        /// <param name="httpContext">The HTTP context.</param>
//        public ViewRenderer(HttpContextWrapper httpContext)
//        {
//            if (httpContext != null)
//                Context = CreateController<EmptyController>(httpContext).ControllerContext;
//            else
//            {
//                throw new InvalidOperationException(
//                    "ViewRenderer must run in the context of an ASP.NET " +
//                    "Application and requires a HttpContext to be present.");
//            }
//        }

//        /// <summary>
//        /// Renders a partial MVC view to string. Use this method to render
//        /// a partial view that doesn't merge with _Layout and doesn't fire
//        /// _ViewStart.
//        /// </summary>
//        /// <param name="viewPath">
//        /// The path to the view to render. Either in same controller, shared by 
//        /// name or as fully qualified ~/ path including extension
//        /// </param>
//        /// <param name="model">The model to pass to the viewRenderer</param>
//        /// <returns>String of the rendered view or null on error</returns>
//        public string RenderPartialViewToString(string viewPath, object model = null)
//        {
//            return RenderViewToStringInternal(viewPath, model, true);
//        }

//        /// <summary>
//        /// Internal method that handles rendering of either partial or 
//        /// or full views.
//        /// </summary>
//        /// <param name="viewPath">
//        /// The path to the view to render. Either in same controller, shared by 
//        /// name or as fully qualified ~/ path including extension
//        /// </param>
//        /// <param name="model">Model to render the view with</param>
//        /// <param name="partial">Determines whether to render a full or partial view</param>
//        /// <returns>String of the rendered view</returns>
//        private string RenderViewToStringInternal(string viewPath, object model,
//                                                    bool partial = false)
//        {
//            // first find the ViewEngine for this view
//            ViewEngineResult viewEngineResult = null;
//            if (partial)
//                viewEngineResult = ViewEngines.Engines.FindPartialView(Context, viewPath);
//            else
//                viewEngineResult = ViewEngines.Engines.FindView(Context, viewPath, null);

//            if (viewEngineResult == null || viewEngineResult.View == null)
//                throw new FileNotFoundException("View could not be found");

//            // get the view and attach the model to view data
//            var view = viewEngineResult.View;
//            Context.Controller.ViewData.Model = model;

//            string result = null;

//            using (var sw = new StringWriter())
//            {
//                var ctx = new ViewContext(Context, view,
//                                            Context.Controller.ViewData,
//                                            Context.Controller.TempData,
//                                            sw);
//                view.Render(ctx, sw);
//                result = sw.ToString();
//            }

//            return result;
//        }


//        /// <summary>
//        /// Creates an instance of an MVC controller from scratch 
//        /// when no existing ControllerContext is present       
//        /// </summary>
//        /// <typeparam name="T">Type of the controller to create</typeparam>
//        /// <returns>Controller for T</returns>
//        public static T CreateController<T>(HttpContextWrapper httpContext, RouteData routeData = null, params object[] parameters)
//                    where T : Controller, new()
//        {
//            // create a disconnected controller instance
//            T controller = (T)Activator.CreateInstance(typeof(T), parameters);

//            if (routeData == null)
//                routeData = new RouteData();

//            bool hasControllerRouteData = routeData.Values.ContainsKey("controller") ||
//                                    routeData.Values.ContainsKey("Controller");
//            if (!hasControllerRouteData)
//            {
//                routeData.Values.Add(ControllerKey, "Stormpath");
//            }

//            controller.ControllerContext = new ControllerContext(httpContext, routeData, controller);
//            return controller;
//        }

//    }

//    /// <summary>
//    /// Empty MVC Controller instance used to 
//    /// instantiate and provide a new ControllerContext
//    /// for the ViewRenderer
//    /// </summary>
//    public class EmptyController : Controller
//    {
//    }
//}