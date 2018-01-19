using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CoruscantMarketplace.DataLayer.Modelos
{
    public class Avaliacao
    {
        [BsonElement("Comentario"), JsonProperty("comentario")]
        public string Comentario { get; set; }
        [BsonElement("Recomendacao"), JsonProperty("recomendacao")]
        public int Recomendacao { get; set; }
    }
}