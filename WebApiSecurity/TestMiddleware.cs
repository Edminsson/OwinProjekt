using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApiSecurity
{
    using Microsoft.Owin;
    using System.IO;
    //using TaskFunc = Func<Dictionary<string, object>, Task>;
    public class TestMiddleware
    {
        private Func<IDictionary<string, object>, Task> _next;
        public TestMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }
        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            Helper.Write("TestMiddleware",context.Request.User);
            await _next(env);
        }
    }

    public class HelloWorldComponent
    {
        Func<IDictionary<string, object>, Task> _next;
        public HelloWorldComponent(Func<IDictionary<string, object>, Task> next)
        {
            this._next = next;
        }
        public async Task Invoke(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);
            Helper.Write("HelloWorld middleware", context.Request.User);

            //var response = environment["owin.ResponseBody"] as Stream;
            //using (StreamWriter writer = new StreamWriter(response))
            //{
            //    await writer.WriteLineAsync("Hello from HelloWorldComponent (Owin middleware)");
            //}

            await this._next(environment);
        }
    }

}