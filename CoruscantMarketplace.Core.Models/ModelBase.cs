using Newtonsoft.Json;

namespace CoruscantMarketplace.Core.Models
{
    /// <summary>
    /// Classe de entidade de negócio base.
    /// Irá representar todas as entidades de negócio
    /// possuem Id
    /// </summary>
    public abstract class ModelBase
    {
        /// <summary>
        /// Identificador único do objeto - PK
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
