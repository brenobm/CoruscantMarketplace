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

        // GET api/produto/produtoslojaprecoporcategoria/a
        /// <summary>
        /// Retorna a lista de todos os produtos por uma categoria trazendo as informações:
        ///  - Nome do Produto
        ///  - Loja do Produto
        ///  - Preço do Produto
        /// </summary>
        /// <param name="categoria">
        /// Categoria que será o filtro dos produtos
        /// </param>
        /// <returns>
        /// Lista de todos os produtos existentes
        /// </returns>
        [HttpGet("produtoslojaprecoporcategoria/{categoria}")]
        [Authorize]
        public IEnumerable<ProdutoLojaPreco> GetProdutosLojaPrecoPorCategoria(string categoria)
        {
            return _produtoBusiness.ListarProdutosLojaPrecoPorCategoria(categoria);
        }

        // GET api/produto/produtoscategoriaprecoporloja/a
        /// <summary>
        /// Retorna a lista de todos os produtos por uma loja trazendo as informações:
        ///  - Nome do Produto
        ///  - Categoria do Produto
        ///  - Preço do Produto
        /// </summary>
        /// <param name="loja">
        /// Loja que será o filtro dos produtos
        /// </param>
        /// <returns>
        /// Lista de todos os produtos existentes
        /// </returns>
        [HttpGet("produtoscategoriaprecoporloja/{loja}")]
        [Authorize]
        public IEnumerable<ProdutoCategoriaPreco> GetProdutosCategoriaPrecoPorLoja(string loja)
        {
            return _produtoBusiness.ListarProdutosCategoriaPrecoPorLoja(loja);
        }

        // GET api/produto/lojas
        /// <summary>
        /// Retorna a lista do nome de todas as lojas
        /// </summary>
        /// <returns>
        /// Lista de todos os nomes das lojas
        /// </returns>
        [HttpGet("lojas")]
        [Authorize]
        public IEnumerable<string> GetLojas()
        {
            return _produtoBusiness.ListarLojas();
        }

        // GET api/produto/categorias
        /// <summary>
        /// Retorna a lista do nome de todas as categorias
        /// </summary>
        /// <returns>
        /// Lista de todos os nomes das categorias
        /// </returns>
        [HttpGet("categorias")]
        [Authorize]
        public IEnumerable<string> GetCategorias()
        {
            return _produtoBusiness.ListarCategorias();
        }
    }
}
