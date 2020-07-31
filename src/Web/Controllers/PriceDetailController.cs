using Microsoft.AspNetCore.Mvc;
using Arbetsprov.Core.Entities;
using Arbetsprov.Infrastructure.Data;
using System.Threading.Tasks;
using Arbetsprov.Application.Interfaces;
using System.Collections.Generic;
using Arbetsprov.Application.DTO;
using System.Diagnostics;

namespace Arbetsprov.Web.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PriceDetailController : ControllerBase
    {
        private readonly IPriceDetailService Service;

        public PriceDetailController(IPriceDetailService service)
        {
            Service = service;
        }

        // GET: api/PriceDetail/:id
        [HttpGet("{sku}")]
        public async Task<ActionResult<IEnumerable<OptimizedPricePeriod>>> GetOptimizedValues(string sku, string market, string currency)
        {
            return Ok(await Service.GetOptimizedPeriodFor(sku, currency, market));
        }
    }
}
