using Newtonsoft.Json;

namespace CoruscantMarketplace.Core.Models
{
    /// <summary>
    /// Representa a entidade de negócio que irá trazer
    /// as informações de Produto / Categoria / Loja / Preço
    /// da entidade Produto
    /// Entidade não possui identificador único, pois 
    /// não é uma entidade persistível, é calculada.
    /// </summary>
    public class ProdutoCategoriaLojaPreco
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("categoria")]
        public string Categoria { get; set; }
        [JsonProperty("loja")]
        public string Loja { get; set; }
        [JsonProperty("preco")]
        public decimal Preco { get; set; }
    }
}
