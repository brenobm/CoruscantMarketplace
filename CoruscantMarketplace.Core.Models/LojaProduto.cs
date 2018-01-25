using Newtonsoft.Json;

namespace CoruscantMarketplace.Core.Models
{
    /// <summary>
    /// Entidade que possui o nome da loja e o preço do produto 
    /// no qual está associado
    /// </summary>
    public class LojaProduto
    {
        /// <summary>
        /// Nome da loja que oferta o produto
        /// </summary>
        [JsonProperty("loja")]
        public string Loja { get; set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        [JsonProperty("preco")]
        public decimal Preco { get; set; }
    }
}