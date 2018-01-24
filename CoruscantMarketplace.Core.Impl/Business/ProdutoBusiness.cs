using CoruscantMarketplace.Core.Business;
using CoruscantMarketplace.Core.Models;
using CoruscantMarketplace.DataLayer.Repositories;
using System.Collections.Generic;

namespace CoruscantMarketplace.Core.Impl.Business
{
    /// <summary>
    /// Implementação da interface para as operações de regras de negócio referente a
    /// entidade Produto
    /// </summary>
    public class ProdutoBusiness : IProdutoBusiness
    {
        /// <summary>
        /// Instância do repositório de dados da entidade Produto
        /// </summary>
        private IProdutoRepository _produtoRepository;

        /// <summary>
        /// Construtor que irá receber os parâmetros via Injeção de Dependência
        /// </summary>
        /// <param name="produtoRepository">
        /// Instância do repositório de dados da entidade Produto
        /// </param>
        public ProdutoBusiness(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        /// <summary>
        /// Atualiza o preço de um produto
        /// </summary>
        /// <param name="id">
        /// Id do produto que terá o preço atualizado
        /// </param>
        /// <param name="preco">
        /// Novo preço do produto
        /// </param>
        public void AtualizarPreco(string id, decimal preco)
        {
            //Cria um objeto de tipo anônimo para passar quais campos serão atualizados
            //Neste caso, somente o preço
            var objetoAtualizacao = new { Preco = preco };
            _produtoRepository.AtualizarCampos(id, objetoAtualizacao);
        }

        /// <summary>
        /// Exclui um produto existente através do seu Id
        /// </summary>
        /// <param name="id">
        /// Id do produto que será excluído
        /// </param>
        public void Excluir(string id)
        {
            _produtoRepository.Excluir(id);
        }

        /// <summary>
        /// Insere um produto novo
        /// </summary>
        /// <param name="produto">
        /// Produto a ser inserido
        /// </param>
        /// <returns>
        /// O objeto Produto inserido com o Id preenchido
        /// </returns>
        public Produto Inserir(Produto produto)
        {
            return _produtoRepository.Inserir(produto);
        }

        /// <summary>
        /// Lista o Nome, Categoria, Loja e Preco de todos os produtos
        /// </summary>
        /// <returns>
        /// Coleção de ProdutoCategoriaLojaPreco
        /// </returns>
        public IEnumerable<ProdutoCategoriaLojaPreco> ListarProdutoCategoriaLojaPreco()
        {
            return _produtoRepository.ListarProdutosCategoriaLojaPreco();
        }

        /// <summary>
        /// Lista todos os produtos
        /// </summary>
        /// <returns>
        /// Coleção de todos os produtos
        /// </returns>
        public IEnumerable<Produto> ListarTodos()
        {
            return _produtoRepository.ListarTodos();
        }
    }
}
