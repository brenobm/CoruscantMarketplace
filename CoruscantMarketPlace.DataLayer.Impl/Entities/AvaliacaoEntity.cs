using MongoDB.Bson.Serialization.Attributes;

namespace CoruscantMarketplace.DataLayer.Impl.Entities
{
    /// <summary>
    /// Representa a entidade Avaliacao persistível no MongoDB
    /// Será persistida sendo composição da entidade Produto
    /// por isso não deve possuir o mecanismo de identificação
    /// (não deve herdar o EntityBase)
    /// </summary>
    public class AvaliacaoEntity
    {
        [BsonElement("comentario")]
        public string Comentario { get; set; }
        [BsonElement("recomendacao")]
        public int Recomendacao { get; set; }
    }
}