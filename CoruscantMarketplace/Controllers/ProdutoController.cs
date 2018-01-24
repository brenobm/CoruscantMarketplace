using CoruscantMarketplace.Core.Business;
using CoruscantMarketplace.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoruscantMarketplace.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private IProdutoBusiness _produtoBusiness;

        public ProdutoController(IProdutoBusiness produtoBusiness)
        {
            _produtoBusiness = produtoBusiness;
        }

        // GET api/produto
        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return _produtoBusiness.ListarTodos();
        }

        // POST api/produto
        [HttpPost]
        public void Post([FromBody]Produto value)
        {
            _produtoBusiness.Inserir(value);
        }

        // PUT api/produto/preco/5
        [HttpPut("preco/{id}")]
        public void AtualizarPreco(string id, [FromBody]decimal preco)
        {
            _produtoBusiness.AtualizarPreco(id, preco);
        }

        // DELETE api/produto/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _produtoBusiness.Excluir(id);
        }

        // GET api/produto/produtocategorialojapreco
        [HttpGet("produtocategorialojapreco")]
        public IEnumerable<ProdutoCategoriaLojaPreco> GetProdutoCategoriaLojaPreco()
        {

            return _produtoBusiness.ListarProdutoCategoriaLojaPreco();
        }
    }
}
