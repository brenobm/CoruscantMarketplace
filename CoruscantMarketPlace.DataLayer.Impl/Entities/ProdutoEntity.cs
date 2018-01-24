using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CoruscantMarketplace.DataLayer.Impl.Entities
{
    /// <summary>
    /// Representa a entidade Produto persistível no MongoDB
    /// Herda do EntityBase para possuir o mecanismo de identificação
    /// do objeto no MongoDB
    /// Cada atributo foi anotado para que o campo no MongoDB seja letra
    /// minúscula
    /// </summary>
    public class ProdutoEntity: EntityBase
    {
        [BsonElement("nome")]
        public string Nome { get; set; }
        [BsonElement("fabricante")]
        public string Fabricante { get; set; }
        [BsonElement("categoria")]
        public string Categoria { get; set; }
        [BsonElement("preco")]
        public decimal Preco { get; set; }
        [BsonElement("loja")]
        public string Loja { get; set; }
        [BsonElement("avaliacao")]
        public IEnumerable<AvaliacaoEntity> Avaliacao { get; set; }
    }
}
