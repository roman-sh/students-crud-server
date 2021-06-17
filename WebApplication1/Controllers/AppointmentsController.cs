using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("appointments")]
    public class AppointmentsController : ApiController
    {
        private Providers providers = new Providers();
        public AppointmentsController()
        {

        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(providers.ProvidersList);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] string specialty, [FromUri] long? date, [FromUri] float? minScore)
        {
            if (string.IsNullOrEmpty(specialty) || date == null || minScore == null)
                return BadRequest();

            return Json(from provider in providers.ProvidersList
                        where provider.specialties.Contains(specialty) &&
                        provider.availableDates.Any(pdate => pdate.@from <= date && pdate.to >= date) &&
                        provider.score >= minScore
                        orderby provider.score ascending
                        select provider);
        }
    }
}
