using CoruscantMarketplace.Core.Models;
using CoruscantMarketplace.DataLayer.Impl.Entities;
using CoruscantMarketplace.DataLayer.Repositories;
using System.Collections.Generic;

namespace CoruscantMarketplace.DataLayer.Impl.Repositories
{
    /// <summary>
    /// Implementação da interface do repositório para a Entidade Produto
    /// Herda os métodos padrões do RepositoryBase e implementa a interface
    /// IProdutoRepository
    /// </summary>
    public class ProdutoRepository : RepositoryBase<Produto, ProdutoEntity>, IProdutoRepository
    {
        /// <summary>
        /// Implementea o método ListarProdutosCategoriaLojaPreco do IProdutoRepository
        /// Lista o Nome, Categoria, Loja e Preco de todos os produtos cadastrados.
        /// A implementação é realizada através do MapReduce
        /// </summary>
        /// <returns>
        /// Coleção de ProdutoCategoriaLojaPreco
        /// </returns>
        public IEnumerable<ProdutoCategoriaLojaPreco> ListarProdutosCategoriaLojaPreco()
        {
            //Função MAP que irá, para todos os objetos, selecionar as propriedades nome, 
            //categoria, loja e preco
            string funcaoMap = @"
                function() {
                    emit(this._id, {Nome: this.nome, Categoria: this.categoria, Loja: this.loja, Preco: this.preco});
                }
            ";

            //Função REDUCE que irá somente retornar o values
            string funcaoReduce = @"
                function(id, values) {
                    return values;
                }
            ";

            //Chama o método MapReduce do RepositoryBase para executar o MapReduce
            //e retornar no resultado no formato ProdutoCategoriaLojaPreco
            return MapReduce<ProdutoCategoriaLojaPreco>(funcaoMap, funcaoReduce);
        }
    }
}
