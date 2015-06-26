using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSecurity
{
    public class HttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            var currentUser = HttpContext.Current.User;
            Helper.Write("HttpModule", currentUser);
        }
    }
}