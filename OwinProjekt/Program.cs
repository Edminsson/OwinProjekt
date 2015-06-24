using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinProjekt
{
    using System.IO;
    using System.Web.Http;
    //Bara ett sätt att slippa skriva den här definitionen varje gång vi behöver den och istället bara ange AppFunc 
    using AppFunc = Func<IDictionary<string, object>, Task>;

    //Vi lägger till följande nuget-paket
    //Microsoft.Owin.Hosting (som installerar några andra)
    //Microsoft.Owin.Host.HttpListener
    //Microsoft.AspNet.WebApi.Owin
    //Microsoft.AspNet.WebApi.OwinSelfHost

    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";
            using(WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stopped!");
            }
        }
    }
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (environment, next) =>
            {
                Console.WriteLine("Request " + environment.Request.Path);
                await next();
                Console.WriteLine("Response " + environment.Response.StatusCode);
            });

            ConfigureWebApi(app);

            app.HelloWorld();
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            app.UseWebApi(config);
        }
    }

    public static class HelloWorldExtensions
    {
        public static void HelloWorld (this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    }

    public class HelloWorldComponent
    {
        AppFunc _next;
        public HelloWorldComponent(Func<IDictionary<string,object>, Task> next)
        {
            this._next = next;
        }
        public Task Invoke(IDictionary<string, object> environment)
        {
            var response = environment["owin.ResponseBody"] as Stream;
            using (StreamWriter writer = new StreamWriter(response))
            {
                return writer.WriteLineAsync("Hello Everybody");
            }
            //await this._next(environment);
        }
    }

}
