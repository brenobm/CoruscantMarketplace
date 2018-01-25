using CoruscantMarketplace.Core.Business;
using CoruscantMarketplace.Core.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IEnumerable<Produto> Get()
        {
            return _produtoBusiness.ListarTodos();
        }

        // POST api/produto
        [HttpPost]
        [Authorize]
        public void Post([FromBody]Produto value)
        {
            _produtoBusiness.Inserir(value);
        }

        // PUT api/produto/preco/5
        [HttpPut("preco/{id}")]
        [Authorize]
        public void AtualizarPreco(string id, [FromBody]decimal preco)
        {
            _produtoBusiness.AtualizarPreco(id, preco);
        }

        // DELETE api/produto/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(string id)
        {
            _produtoBusiness.Excluir(id);
        }

        // GET api/produto/produtocategorialojapreco
        [HttpGet("produtocategorialojapreco")]
        [Authorize]
        public IEnumerable<ProdutoCategoriaLojaPreco> GetProdutoCategoriaLojaPreco()
        {

            return _produtoBusiness.ListarProdutoCategoriaLojaPreco();
        }
    }
}
