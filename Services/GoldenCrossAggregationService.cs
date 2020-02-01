using System.Collections.Generic;
using BacktraderDataApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using MongoDB.Bson.Serialization;

namespace BacktraderDataApi.service
{
    public class GoldenCrossAggregationService
    {
        private readonly IMongoCollection<GoldenCross> _strategies;

        public GoldenCrossAggregationService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _strategies = database.GetCollection<GoldenCross>(settings.CollectionName);
        }
        public async Task<List<AggregateTop10Result>> Get()
        {
            var result = await _strategies.Aggregate().Group(x => x.buyDate.Year, g => new AggregateTop10Result()
            {
                year = g.Key,
                count = g.Count()
            }
            )
            .SortByDescending(x => x.year)
            .ToListAsync<AggregateTop10Result>();

            return result;
        }
    }
}