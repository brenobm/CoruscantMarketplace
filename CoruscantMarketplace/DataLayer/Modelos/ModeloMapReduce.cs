using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoruscantMarketplace.DataLayer.Modelos
{
    public class ModeloMapReduce<T>
    {
        public ObjectId Id { get; set; }
        [BsonElement("value")]
        public T Value { get; set; }
    }
}
