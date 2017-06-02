using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestMVC5.Controllers
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Now => DateTime.Now.ToString("T");
    }


    [LogFilter]
    public class HomeController : Controller
    {
        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            var ab = new User
            {
                FirstName = "AAA",
                LastName = "BBBB"
            };

            return Json(ab, JsonRequestBehavior.AllowGet);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
    }


    public class LogFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            Debug.WriteLine("In On actionExecuted ");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values[key: "controller"];
            var actionName = filterContext.RouteData.Values[key: "action"];
            var url = filterContext.HttpContext.Request.Url;
            var message = $"Executing - controller:{controllerName} action:{actionName}";
            Debug.WriteLine(message);
            base.OnActionExecuting(filterContext: filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Debug.WriteLine("In OnResultExecuting ");
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values[key: "controller"];
            var actionName = filterContext.RouteData.Values[key: "action"];
            var message = $"Executed - controller:{controllerName} action:{actionName}";

            Debug.WriteLine(message);
            base.OnResultExecuted(filterContext: filterContext);
        }
    }
}