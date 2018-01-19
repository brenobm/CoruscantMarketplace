using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoruscantMarketplace.DataLayer.Modelos
{
    public class Produto: ModeloBase
    {
        [BsonElement("Nome"), JsonProperty("nome")]
        public string Nome { get; set; }
        [BsonElement("Fabricante"), JsonProperty("fabricante")]
        public string Fabricante { get; set; }
        [BsonElement("Categoria"), JsonProperty("categoria")]
        public string Categoria { get; set; }
        [BsonElement("Preco"), JsonProperty("preco")]
        public decimal Preco { get; set; }
        [BsonElement("Loja"), JsonProperty("loja")]
        public string Loja { get; set; }
        [BsonElement("Avaliacao"), JsonProperty("avaliacao")]
        public IEnumerable<Avaliacao> Avaliacao { get; set; }
    }
}
