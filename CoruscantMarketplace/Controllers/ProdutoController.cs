using CoruscantMarketplace.Core.Business;
using CoruscantMarketplace.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoruscantMarketplace.Controllers
{
    /// <summary>
    /// Web.API para os serviços disponíveis para o Produto
    /// </summary>
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private IProdutoBusiness _produtoBusiness;

        /// <summary>
        /// Construtor que recebe via Injeção de Dependência as dependências necessárias
        /// </summary>
        /// <param name="produtoBusiness">
        /// Instância do IProdutoBusiness
        /// </param>
        public ProdutoController(IProdutoBusiness produtoBusiness)
        {
            _produtoBusiness = produtoBusiness;
        }

        // GET api/produto
        /// <summary>
        /// Retorna todos os produtos cadastrados.
        /// 
        /// Necessita autenticação através de header.
        /// Ex: "Authorization: Bearer {token}"
        /// 
        /// Para a geração do Token acessar a API "api/AuthToken/" [GET]
        /// </summary>
        /// <returns>
        /// Lista de todos os produtos.
        /// </returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<Produto> Get()
        {
            return _produtoBusiness.ListarTodos();
        }

        // POST api/produto
        /// <summary>
        /// Insere um novo Produto
        /// 
        /// Necessita autenticação através de header.
        /// Ex: "Authorization: Bearer {token}"
        /// 
        /// Para a geração do Token acessar a API "api/AuthToken/" [GET]
        /// </summary>
        /// <param name="value">
        /// Produto a ser inserido
        /// </param>
        [HttpPost]
        [Authorize]
        public void Post([FromBody]Produto value)
        {
            _produtoBusiness.Inserir(value);
        }

        // PUT api/produto/preco/5
        /// <summary>
        /// Atualiza o preço de um produto existente através do seu ID
        /// 
        /// Necessita autenticação através de header.
        /// Ex: "Authorization: Bearer {token}"
        /// 
        /// Para a geração do Token acessar a API "api/AuthToken/" [GET]
        /// </summary>
        /// <param name="id">
        /// Id do produto que será atualizado
        /// </param>
        /// <param name="preco">
        /// Novo preço do produto
        /// </param>
        [HttpPut("preco/{id}")]
        [Authorize]
        public void AtualizarPreco(string id, [FromBody]decimal preco)
        {
            _produtoBusiness.AtualizarPreco(id, preco);
        }

        // DELETE api/produto/5
        /// <summary>
        /// Exclui um produto existente através do seu ID
        /// 
        /// Necessita autenticação através de header.
        /// Ex: "Authorization: Bearer {token}"
        /// 
        /// Para a geração do Token acessar a API "api/AuthToken/" [GET]
        /// </summary>
        /// <param name="id">
        /// Id do produto a ser excluído
        /// </param>
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(string id)
        {
            _produtoBusiness.Excluir(id);
        }

        // GET api/produto/produtocategorialojapreco
        /// <summary>
        /// Retorna a lista de todos os produtos existentes trazendo as informações:
        ///  - Nome do Produto
        ///  - Categoria do Produto
        ///  - Loja do Produto
        ///  - Preço do Produto
        /// </summary>
        /// <returns>
        /// Lista de todos os produtos existentes
        /// </returns>
        [HttpGet("produtocategorialojapreco")]
        [Authorize]
        public IEnumerable<ProdutoCategoriaLojaPreco> GetProdutoCategoriaLojaPreco()
        {

            return _produtoBusiness.ListarProdutoCategoriaLojaPreco();
        }
    }
}
