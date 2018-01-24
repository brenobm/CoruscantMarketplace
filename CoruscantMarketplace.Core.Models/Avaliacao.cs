using Newtonsoft.Json;

namespace CoruscantMarketplace.Core.Models
{
    /// <summary>
    /// Representa a entidade de negócio Avaliacao
    /// Entidade não possui identificador único.
    /// Possui uma relação de composição com o Produto
    /// </summary>
    public class Avaliacao
    {
        /// <summary>
        /// Comentário sobre o produto
        /// </summary>
        [JsonProperty("comentario")]
        public string Comentario { get; set; }

        /// <summary>
        /// Índice de recomendação do produto.
        /// De 1 à 5
        /// </summary>
        [JsonProperty("recomendacao")]
        public byte Recomendacao { get; set; }
    }
}