using CoruscantMarketplace.Core.Models;
using CoruscantMarketplace.Crosscutting;
using CoruscantMarketplace.Crosscutting.Exceptions;
using CoruscantMarketplace.DataLayer.Impl.Entities;
using CoruscantMarketplace.DataLayer.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

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
        /// Construtor para injeção de dependência
        /// </summary>
        /// <param name="logger">
        /// Instancia do mecanismo de logging que será inserido via injeção de dependência
        /// </param>
        public ProdutoRepository(ILogger<RepositoryBase<Produto, ProdutoEntity>> logger) 
            : base(logger)
        {
        }

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

            try
            {
                //Chama o método MapReduce do RepositoryBase para executar o MapReduce
                //e retornar no resultado no formato ProdutoCategoriaLojaPreco
                return MapReduce<ProdutoCategoriaLojaPreco>(funcaoMap, funcaoReduce);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao executar MapReduce do ListarProdutosCategoriaLojaPreco");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar MapReduce do ListarProdutosCategoriaLojaPreco");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos.", ex));
            }
        }
    }
}
