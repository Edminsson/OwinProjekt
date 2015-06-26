using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebApiSecurity.Startup))]

namespace WebApiSecurity
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}");
            //app.Use(typeof(TestMiddleware));
            app.Use<HelloWorldComponent>();
            app.Use<TestMiddleware>();

            app.UseWebApi(config);
        }
    }
}
