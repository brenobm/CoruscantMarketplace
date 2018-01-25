using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoruscantMarketplace.Core.Models
{
    /// <summary>
    /// Representa a entidade de negócio que irá trazer
    /// as informações de Produto / Categoria / Preço
    /// da entidade Produto
    /// Entidade não possui identificador único, pois 
    /// não é uma entidade persistível, é calculada.
    /// </summary>
    public class ProdutoCategoriaPreco
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("categoria")]
        public string Categoria { get; set; }
        [JsonProperty("preco")]
        public decimal Preco { get; set; }
    }
}
