using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using MunicipalitiesTaxes.GlobalException;

namespace MunicipalitiesTaxes
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
#if DEBUG
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler(true));
#else
            
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler(false));
#endif

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
