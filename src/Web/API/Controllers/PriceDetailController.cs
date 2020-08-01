using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Arbetsprov.Application.Interfaces;
using System.Collections.Generic;
using Arbetsprov.Application.DTO;

namespace Arbetsprov.Web.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PriceDetailController : ControllerBase
    {
        private readonly IPriceDetailService Service;

        public PriceDetailController(IPriceDetailService service)
        {
            Service = service;
        }

        // GET: api/PriceDetail/:id?market&currency
        [HttpGet("{sku}")]
        public async Task<ActionResult<IEnumerable<OptimizedPricePeriod>>> GetOptimizedValues(string sku, string market, string currency)
        {
            return Ok(await Service.GetOptimizedPeriodFor(sku, currency, market));
        }
    }
}
