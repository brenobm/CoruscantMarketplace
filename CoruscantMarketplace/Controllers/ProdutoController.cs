using CoruscantMarketplace.DataLayer.Modelos;
using CoruscantMarketplace.DataLayer.Repositorios;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;

namespace CoruscantMarketplace.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private IRepositorio<Produto> _repProduto;

        public ProdutoController()
        {
            _repProduto = new Repositorio<Produto>();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return _repProduto.ListarTodos();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Produto value)
        {
            _repProduto.Inserir(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPut("preco/{id}")]
        public void AtualizarPreco(string id, [FromBody]decimal preco)
        {
            ObjectId objectId = new ObjectId(id);

            _repProduto.AtualizarCampos(objectId, new { Preco = preco });
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ObjectId objectId = new ObjectId(id);

            _repProduto.Excluir(objectId);
        }

        [HttpGet("produtocategorialojapreco")]
        public IEnumerable<ProdutoCategoriaLojaPreco> GetProdutoCategoriaLojaPreco()
        {
            string funcaoMap = @"
                function() {
                    emit(this._id, {Nome: this.Nome, Categoria: this.Categoria, Loja: this.Loja, Preco: this.Preco});
                }
            ";

            string funcaoReduce = @"
                function(id, values) {
                    return values;
                }
            ";

            return _repProduto.MapReduce<ProdutoCategoriaLojaPreco>(funcaoMap, funcaoReduce);
        }
    }
}
