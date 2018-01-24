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
    }
}
