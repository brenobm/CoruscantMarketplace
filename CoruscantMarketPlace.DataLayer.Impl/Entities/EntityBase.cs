using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoruscantMarketplace.DataLayer.Impl.Entities
{
    /// <summary>
    /// Classe base para as entidades que serão persistidas no MongoDB
    /// Possuindo o mecanismo básico de identificação do objeto
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Identificador da entidade para o MongoDB
        /// </summary>
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        
        /// <summary>
        /// Tradutor do Id (ObjectId) para String
        /// </summary>
        [BsonIgnore]
        public string IdString
        {
            get
            {
                return Id.ToString();
            }

            set
            {
                Id = new ObjectId(value);
            }
        }
    }
}