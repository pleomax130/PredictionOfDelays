using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PredictionOfDelays.Api.Controllers
{
    public class TestController : ApiController
    {
        public string[] Get()
        {
            return new [] {"hehe"};
        }
    }
}
