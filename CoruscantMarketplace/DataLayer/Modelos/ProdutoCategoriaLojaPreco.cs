using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoruscantMarketplace.DataLayer.Modelos
{
    public class ProdutoCategoriaLojaPreco
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("categoria")]
        public string Categoria { get; set; }
        [JsonProperty("loja")]
        public string Loja { get; set; }
        [JsonProperty("preco")]
        public string Preco { get; set; }
    }
}
