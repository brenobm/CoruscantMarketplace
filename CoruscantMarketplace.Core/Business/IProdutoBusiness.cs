using CoruscantMarketplace.Core.Models;
using System.Collections.Generic;

namespace CoruscantMarketplace.Core.Business
{
    /// <summary>
    /// Interface para as operações de regras de negócio referente a
    /// entidade Produto
    /// </summary>
    public interface IProdutoBusiness
    {
        /// <summary>
        /// Lista todos os produtos
        /// </summary>
        /// <returns>
        /// Coleção de todos os produtos
        /// </returns>
        IEnumerable<Produto> ListarTodos();
        /// <summary>
        /// Insere um produto novo
        /// </summary>
        /// <param name="produto">
        /// Produto a ser inserido
        /// </param>
        /// <returns>
        /// O objeto Produto inserido com o Id preenchido
        /// </returns>
        Produto Inserir(Produto produto);
        /// <summary>
        /// Exclui um produto existente através do seu Id
        /// </summary>
        /// <param name="id">
        /// Id do produto que será excluído
        /// </param>
        void Excluir(string id);
        /// <summary>
        /// Atualiza o preço de um produto
        /// </summary>
        /// <param name="id">
        /// Id do produto que terá o preço atualizado
        /// </param>
        /// <param name="preco">
        /// Novo preço do produto
        /// </param>
        void AtualizarPreco(string id, decimal preco);
        /// <summary>
        /// Lista o Nome, Categoria, Loja e Preco de todos os produtos
        /// </summary>
        /// <returns>
        /// Coleção de ProdutoCategoriaLojaPreco
        /// </returns>
        IEnumerable<ProdutoCategoriaLojaPreco> ListarProdutoCategoriaLojaPreco();
    }
}
