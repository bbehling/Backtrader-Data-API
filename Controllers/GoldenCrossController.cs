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

        public GoldenCrossController(ILogger<GoldenCrossController> logger, GoldenCrossService goldenCrossService)
        {
            _goldenCrossService = goldenCrossService;
        }

        [HttpPost]
        public async Task<ActionResult<List<GoldenCross>>> PostAsync([FromBody]PagedQuery query)
        {
            return await _goldenCrossService.Get(query.pageNumber, query.count, query.minDate);
        }
    }
}
