using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BacktraderDataApi.Models
{
    public class GoldenCross
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime buyDate { get; set; }

        public string ticker { get; set; }


    }

    public class Result
    {
        public List<GoldenCross> goldenCrosses { get; set; }
        public long totalCount { get; set; } = 0;
    }
}