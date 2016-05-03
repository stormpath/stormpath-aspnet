﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stormpath.AspNet;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        public string Get()
        {
            var account = Request.GetStormpathAccount();

            return account?.FullName;
        }
    }
}
