using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoruscantMarketplace.Core.Models
{
    /// <summary>
    /// Retorna o resumo de um produto
    /// </summary>
    public class ResumoProduto
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
        /// Média da recomendação do produto
        /// </summary>
        [JsonProperty("avaliacao")]
        public int Avaliacao { get; set; }

        /// <summary>
        /// Coleção com o nome da loja e o preço
        /// </summary>
        [JsonProperty("lojas")]
        public IEnumerable<LojaProduto> Lojas { get; set; }
    }
}
