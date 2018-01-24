using MongoDB.Bson.Serialization.Attributes;

namespace CoruscantMarketplace.DataLayer.Impl.Entities
{
    /// <summary>
    /// Entidade genérica para representar o resultado
    /// do MapReduce do MongoDB
    /// </summary>
    /// <typeparam name="T">
    /// Entidade que será retornada no resultado do MapResult
    /// </typeparam>
    public class EntityMapReduce<T>: EntityBase
    {
        /// <summary>
        /// Valor de sumarização do retorno do MapReduce
        /// </summary>
        [BsonElement("value")]
        public T Value { get; set; }
    }
}
