using Newtonsoft.Json;

namespace CoruscantMarketplace.Core.Models
{
    /// <summary>
    /// Representa a entidade de negócio que irá trazer
    /// as informações de Produto / Loja / Preço
    /// da entidade Produto
    /// Entidade não possui identificador único, pois 
    /// não é uma entidade persistível, é calculada.
    /// </summary>
    public class ProdutoLojaPreco
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("loja")]
        public string Loja { get; set; }
        [JsonProperty("preco")]
        public decimal Preco { get; set; }
    }
}
