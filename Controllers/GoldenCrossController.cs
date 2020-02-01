using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BacktraderDataApi.service;
using Newtonsoft.Json.Linq;
using BacktraderDataApi.Models;

namespace BacktraderDataApi.Controllers
{
    [ApiController]
    [Route("apiV1/[controller]")]
    public class GoldenCrossController : ControllerBase
    {

        private readonly GoldenCrossService _goldenCrossService;
        private readonly GoldenCrossAggregationService _goldenCrossAggregationService;

        public GoldenCrossController(ILogger<GoldenCrossController> logger, GoldenCrossService goldenCrossService,
            GoldenCrossAggregationService goldenCrossAggregationService)
        {
            _goldenCrossService = goldenCrossService;
            _goldenCrossAggregationService = goldenCrossAggregationService;
        }

        [HttpPost("date")]
        public async Task<ActionResult<Result>> Date([FromBody]PagedQuery query)
        {
            return await _goldenCrossService.Get(query.pageIndex, query.pageSize, query.minDate, query.maxDate);
        }
        [HttpGet("top10")]
        public async Task<ActionResult<List<AggregateTop10Result>>> top10()
        {
            return await _goldenCrossAggregationService.Get();
        }
    }
}
