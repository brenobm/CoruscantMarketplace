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

        /// <summary>
        /// Lista o Nome, Loja e Preco de produtos de uma determinada categoria
        /// </summary>
        /// <param name="categoria">
        /// Categoria que será o filtro dos produtos
        /// </param>
        /// <returns>
        /// Coleção de ProdutoLojaPreco
        /// </returns>
        IEnumerable<ProdutoLojaPreco> ListarProdutosLojaPrecoPorCategoria(string categoria);

        /// <summary>
        /// Lista o Nome, Categoria e Preco de produtos de uma determinada loja
        /// </summary>
        /// <param name="loja">
        /// Loja que será o filtro dos produtos
        /// </param>
        /// <returns>
        /// Coleção de ProdutoCategoriaPreco
        ///</returns>
        IEnumerable<ProdutoCategoriaPreco> ListarProdutosCategoriaPrecoPorLoja(string loja);

        /// <summary>
        /// Lista todas as lojas
        /// </summary>
        /// <returns>
        /// Traz o nome de todas as lojas
        /// </returns>
        IEnumerable<string> ListarLojas();

        /// <summary>
        /// Lista todas as categorias
        /// </summary>
        /// <returns>
        /// Traz o nome de todas as categorias
        /// </returns>
        IEnumerable<string> ListarCategorias();

        /// <summary>
        /// Lista o resumo do produto trazendo m nome, fabricante, categoria, sua
        /// avaliação média e uma lista contendo todos os preços e as lojas em que o objeto é vendido        
        /// </summary>
        /// <param name="produto">
        /// Nome do produto
        /// </param>
        /// <returns>
        /// Coleção de ResumoProduto
        /// </returns>
        IEnumerable<ResumoProduto> ObterResumoProduto(string produto);
    }
}
