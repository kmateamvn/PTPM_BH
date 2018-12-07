using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace PTPM_HTQLNH_DASHBOARD.Routing
{
    public class CustomRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {


            return new CustomHttpHandler(requestContext);
        }
    }
    public class CustomHttpHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public RequestContext RequestContext { get; set; }

        public CustomHttpHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            string acitonName = RequestContext.RouteData.Values["action"].ToString();
            string controllerName = RequestContext.RouteData.Values["controller"].ToString();
             
            gotoController(controllerName,  acitonName);
            return;
            
        }

        private void gotoController(string controllerName, string action = null)
        {
            IController controller = null;
            IControllerFactory factory = null;
            try
            {
                factory = ControllerBuilder.Current.GetControllerFactory();
                controller = factory.CreateController(RequestContext, controllerName);
                if (controller != null)
                {
                    RequestContext.RouteData.Values["controller"] = controllerName;
                    if (action != null)
                    {
                        RequestContext.RouteData.Values["action"] = action;
                    }
                    controller.Execute(RequestContext);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }
            finally
            {
                factory.ReleaseController(controller);
            }
        }
    }
}
