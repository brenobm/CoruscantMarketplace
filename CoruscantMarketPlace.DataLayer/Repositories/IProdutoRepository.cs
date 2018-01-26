using CoruscantMarketplace.Core.Models;
using System.Collections.Generic;

namespace CoruscantMarketplace.DataLayer.Repositories
{
    /// <summary>
    /// Interface do repositório para a Entidade Produto
    /// Herda todos os métodos básicos para todas as entidade
    /// e possui os métodos específicos para a entidade Produto
    /// </summary>
    public interface IProdutoRepository: IRepositoryBase<Produto>
    {
        /// <summary>
        /// Lista o Nome, Categoria, Loja e Preco de todos os produtos
        /// cadastrados
        /// </summary>
        /// <returns>
        /// Coleção de ProdutoCategoriaLojaPreco
        /// </returns>
        IEnumerable<ProdutoCategoriaLojaPreco> ListarProdutosCategoriaLojaPreco();

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
        /// </returns>
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
