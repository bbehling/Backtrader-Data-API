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

        public async Task<Result> Get(int pageIndex, int pageSize, string minDate, string maxDate)
        {

            var myArray = new JArray();

            var result = new Result();
            result.goldenCrosses = new List<GoldenCross>();

            // TODO - clean up this IF statement junk. First change ParseExact to TryParseExact
            if (string.IsNullOrEmpty(maxDate) && !string.IsNullOrEmpty(minDate))
            {
                var min = DateTime.ParseExact(minDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                var max = min.AddDays(1);

                result.totalCount = _strategies.Find(x =>
                                    x.buyDate >= min &
                                    x.buyDate < max).CountDocuments();

                if (result.totalCount > 0)
                {
                    result.goldenCrosses = await _strategies.Find(x =>
                                    x.buyDate >= min &
                                    x.buyDate < max)
                                    .SortBy(x => x.buyDate)
                                .Skip(pageIndex > 0 ? ((pageIndex) * pageSize) : 0)
                                .Limit(pageSize).ToListAsync<GoldenCross>();
                }

                return result;
            }
            else if (!string.IsNullOrEmpty(maxDate) && !string.IsNullOrEmpty(minDate))
            {
                var min = DateTime.ParseExact(minDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                var max = DateTime.ParseExact(maxDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                result.totalCount = _strategies.Find(x =>
                                    x.buyDate >= min &
                                    x.buyDate < max).CountDocuments();
                if (result.totalCount > 0)
                {
                    result.goldenCrosses = await _strategies.Find(x =>
                                                        x.buyDate >= min &
                                                        x.buyDate <= max)
                                                    .SortBy(x => x.buyDate)
                                                    .Skip(pageIndex > 0 ? ((pageIndex) * pageSize) : 0)
                                                    .Limit(pageSize).ToListAsync<GoldenCross>();
                }


                return result;
            }
            else
            {
                return result;
            }
        }
    }
}