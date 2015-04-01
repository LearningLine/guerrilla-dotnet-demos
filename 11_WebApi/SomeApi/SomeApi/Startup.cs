using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SomeApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute("api",
            //    "{controller}/{id}", new
            //    {
            //        id = RouteParameter.Optional
            //    });
            app.UseWebApi(config);
        }
    }
}