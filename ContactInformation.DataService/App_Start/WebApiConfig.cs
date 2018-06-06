using ContactInformation.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
//using System.Web.OData;
//using System.Web.OData.Builder;
//using System.Web.OData.Extensions;

namespace ContactInformation.DataService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web Api Routes

           

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            

            //ODataModelBuilder builder = new ODataModelBuilder();
            //builder.EntitySet<Customer>("Customers");
            //builder.EntitySet<Customer>("CustomerContacts");
            //builder.EntitySet<Customer>("ContactTypes");

            //config.MapODataServiceRoute(
            //    routeName: "ODataRoute",
            //    routePrefix: "DaaS/CC",
            //    model: builder.GetEdmModel(),
            //    defaultHandler: new ODataNullValueMessageHandler()
            //    {
            //        InnerHandler = new HttpControllerDispatcher(config)
            //    });
        }
    }
}
