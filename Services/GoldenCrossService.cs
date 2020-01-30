using System.Collections.Generic;
using BacktraderDataApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Globalization;

namespace BacktraderDataApi.service
{
    public class GoldenCrossService
    {
        private readonly IMongoCollection<GoldenCross> _strategies;

        public GoldenCrossService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _strategies = database.GetCollection<GoldenCross>(settings.CollectionName);
        }

        public async Task<List<GoldenCross>> Get(int pageNumber, int nPerPage, string minDate)
        {
            var min = DateTime.ParseExact(minDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var max = new DateTime(2020, 01, 27, 23, 0, 0);

            var myArray = new JArray();
            var json2 = await _strategies.Find(x =>
                    x.buyDate > min &
                    x.buyDate < max)
                .Skip(pageNumber > 0 ? ((pageNumber - 1) * nPerPage) : 0)
                .Limit(nPerPage).ToListAsync<GoldenCross>();

            return json2;
        }
    }
}