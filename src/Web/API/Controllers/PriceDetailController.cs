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

        // GET: api/PriceDetail/:id/markets
        [HttpGet("{sku}/markets")]
        public async Task<ActionResult<IEnumerable<string>>> GetMarkets(string sku)
        {
            if (!await Service.Exists(sku))
            {
                return NotFound();
            }
            return Ok(await Service.GetMarketsFor(sku));
        }

        // GET: api/PriceDetail/:id/currencies
        [HttpGet("{sku}/currencies")]
        public async Task<ActionResult<IEnumerable<string>>> GetCurrencies(string sku)
        {
            if (!await Service.Exists(sku))
            {
                return NotFound();
            }
            return Ok(await Service.GetCurrenciesFor(sku));
        }

        // GET: api/PriceDetail/:id/exists
        [HttpGet("{sku}/exists")]
        public async Task<ActionResult<bool>> Exists(string sku)
        {
            return (await Service.Exists(sku)) ? Ok(true) : (ActionResult)NotFound();
        }

        // GET: api/PriceDetail/:id/optimizedvalues?market&currency
        [HttpGet("{sku}/optimizedvalues")]
        public async Task<ActionResult<IEnumerable<OptimizedPricePeriod>>> GetOptimizedValues(string sku, string market, string currency)
        {
            if (!await Service.Exists(sku))
            {
                return NotFound();
            }
            return Ok(await Service.GetOptimizedPeriodFor(sku, currency, market));
        }
    }
}
