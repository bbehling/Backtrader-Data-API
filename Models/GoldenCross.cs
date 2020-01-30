using System;
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
}