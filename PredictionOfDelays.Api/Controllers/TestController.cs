using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Api.Controllers
{
    public class TestController : ApiController
    {
        public string Get()
        {
            ApplicationDbContext t = new ApplicationDbContext();
            t.Users.ToList();
            return "halo";
        }
    }
}
