using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Models;
using System;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public partial class OffersController : Controller
    {
        [HttpPost]
        [Route("/api/offers/doSomeThingDifferentWeCantAutoGenerate")]
        public IActionResult ThisWillNeverBeOverridden([FromBody]OfferDTO offerDTO)
        {
            try
            {
                return Ok("Something crazy!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
