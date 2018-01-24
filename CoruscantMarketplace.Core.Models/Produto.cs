using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoruscantMarketplace.Core.Models
{
    /// <summary>
    /// Representa a entidade de negócio Produto
    /// Entidade que possui Identificador Único
    /// </summary>
    public class Produto: ModelBase
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        [JsonProperty("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Nome do fabricante do produto
        /// </summary>
        [JsonProperty("fabricante")]
        public string Fabricante { get; set; }

        /// <summary>
        /// Nome da categoria do produto
        /// </summary>
        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        [JsonProperty("preco")]
        public decimal Preco { get; set; }

        /// <summary>
        /// Nome da loja que oferta o produto
        /// </summary>
        [JsonProperty("loja")]
        public string Loja { get; set; }

        /// <summary>
        /// Coleção de avaliações feitas sobre o produto
        /// </summary>
        [JsonProperty("avaliacao")]
        public IEnumerable<Avaliacao> Avaliacao { get; set; }
    }
}
