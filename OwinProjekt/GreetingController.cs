﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinConsole
{
    public class GreetingController : ApiController
    {
        public Greeting Get()
        {
            return new Greeting() { Text = "Hello from Web Api" };
        }
    }
}
