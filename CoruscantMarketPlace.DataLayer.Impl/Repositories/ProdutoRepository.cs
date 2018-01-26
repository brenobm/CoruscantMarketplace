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
        /// Implementea o método categorias do IProdutoRepository
        /// Lista todas as categorias
        /// </summary>
        /// <returns>
        /// Traz o nome de todas as categorias
        /// </returns>
        public IEnumerable<string> ListarCategorias()
        {
            //Função MAP que irá, para todos os objetos e selecionar a propriedade categoria
            string funcaoMap = @"
                function() {
                    emit(this.categoria, this.categoria);
                }
            ";

            //Função REDUCE que irá somente retornar o id - nome da categoria
            string funcaoReduce = @"
                function(id, values) {
                    return id;
                }
            ";

            try
            {
                //Chama o método MapReduce do RepositoryBase para executar o MapReduce
                //e retornar no resultado o nome da categoria como string
                return MapReduce<string>(funcaoMap, funcaoReduce);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao executar MapReduce do ListarCategorias");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar MapReduce do ListarCategorias");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos.", ex));
            }
        }

        /// <summary>
        /// Implementea o método ListarLojas do IProdutoRepository
        /// Lista todas as lojas
        /// </summary>
        /// <returns>
        /// Traz o nome de todas as lojas
        /// </returns>
        public IEnumerable<string> ListarLojas()
        {
            //Função MAP que irá, para todos os objetos e selecionar a propriedade loja
            string funcaoMap = @"
                function() {
                    emit(this.loja, this.loja);
                }
            ";

            //Função REDUCE que irá somente retornar o id - nome da loja
            string funcaoReduce = @"
                function(id, values) {
                    return id;
                }
            ";

            try
            {
                //Chama o método MapReduce do RepositoryBase para executar o MapReduce
                //e retornar no resultado o nome da loja como string
                return MapReduce<string>(funcaoMap, funcaoReduce);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao executar MapReduce do ListarLojas");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar MapReduce do ListarLojas");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos.", ex));
            }
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

        /// <summary>
        /// Implementea o método ListarProdutosCategoriaPrecoPorLoja do IProdutoRepository
        /// Lista o Nome, Categoria e Preco de produtos de uma determinada loja
        /// </summary>
        /// <param name="loja">
        /// Loja que será o filtro dos produtos
        /// </param>
        /// <returns>
        /// Coleção de ProdutoCategoriaPreco
        /// </returns>
        public IEnumerable<ProdutoCategoriaPreco> ListarProdutosCategoriaPrecoPorLoja(string loja)
        {
            //Função MAP que irá, para todos os objetos, selecionar as propriedades nome, 
            //categoria e preco
            string funcaoMap = @"
                function() {
                    emit(this._id, {Nome: this.nome, Categoria: this.categoria, Preco: this.preco});
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
                // Filtra por loja
                MapReduceOptions<ProdutoEntity, EntityMapReduce<ProdutoCategoriaPreco>> options = new MapReduceOptions<ProdutoEntity, EntityMapReduce<ProdutoCategoriaPreco>>
                {
                    Filter = Builders<ProdutoEntity>.Filter.Where(p => p.Loja == loja)
                };

                //Chama o método MapReduce do RepositoryBase para executar o MapReduce
                //e retornar no resultado no formato ProdutoLojaPreco
                return MapReduce<ProdutoCategoriaPreco>(funcaoMap, funcaoReduce, options);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao executar MapReduce do ListarProdutosLojaPrecoPorCategoria");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar MapReduce do ListarProdutosLojaPrecoPorCategoria");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos.", ex));
            }
        }

        /// <summary>
        /// Implementea o método ListarProdutosLojaPrecoPorCategoria do IProdutoRepository
        /// Lista o Nome, Loja e Preco de produtos de uma determinada categoria
        /// </summary>
        /// <param name="categoria">
        /// Categoria que será o filtro dos produtos
        /// </param>
        /// <returns>
        /// Coleção de ProdutoLojaPreco
        /// </returns>
        public IEnumerable<ProdutoLojaPreco> ListarProdutosLojaPrecoPorCategoria(string categoria)
        {
            //Função MAP que irá, para todos os objetos, selecionar as propriedades nome, 
            //loja e preco
            string funcaoMap = @"
                function() {
                    emit(this._id, {Nome: this.nome, Loja: this.loja, Preco: this.preco});
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
                // Filtra por categoria
                MapReduceOptions<ProdutoEntity, EntityMapReduce<ProdutoLojaPreco>> options = new MapReduceOptions<ProdutoEntity, EntityMapReduce<ProdutoLojaPreco>>
                {
                    Filter = Builders<ProdutoEntity>.Filter.Where(p => p.Categoria == categoria)
                };

                //Chama o método MapReduce do RepositoryBase para executar o MapReduce
                //e retornar no resultado no formato ProdutoLojaPreco
                return MapReduce<ProdutoLojaPreco>(funcaoMap, funcaoReduce, options);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao executar MapReduce do ListarProdutosLojaPrecoPorCategoria");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar MapReduce do ListarProdutosLojaPrecoPorCategoria");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos.", ex));
            }

        }

        /// <summary>
        /// Implementea o método ObterResumoProduto do IProdutoRepository
        /// Lista o resumo do produto trazendo m nome, fabricante, categoria, sua
        /// avaliação média e uma lista contendo todos os preços e as lojas em que o objeto é vendido        
        /// </summary>
        /// <param name="produto">
        /// Nome do produto
        /// </param>
        /// <returns>
        /// Coleção de ResumoProduto
        /// </returns>
        public IEnumerable<ResumoProduto> ObterResumoProduto(string produto)
        {
            // Função MAP estrutura o objeto produto no formato do ResumoProduto
            // usando o nome do produto como chave.
            string funcaoMap = @"
                function() {

                    var somatoriaRecomendacao = 0;
                    var quantidadeRecomendacao = 0;

                    function somaAvaliacao (total, avaliacao) {
                        return total + avaliacao.recomendacao;
                    }

                    somatoriaRecomendacao = this.avaliacao.reduce(somaAvaliacao, 0);
                    quantidadeRecomendacao = this.avaliacao.length;

                    var resumo = {
                        Nome: this.nome,
                        Categoria: this.categoria,
                        Fabricante: this.fabricante,
                        Lojas: [{
                            Loja: this.loja, 
                            Preco: this.preco
                        }],
                        Avaliacao: somatoriaRecomendacao / quantidadeRecomendacao
                    };


                    emit(this.nome, resumo);
                }
            ";

            // Função REDUCE faz a agregação das Lojas e média das avaliações
            string funcaoReduce = @"
                function(id, values) {
                    var lojas = [];
                    var somatoriaRecomendacao = 0;
                    var quantidadeRecomendacao = 0;

                    values.forEach (function (value) {
                        lojas.push(value.Lojas[0]);
                        
                        somatoriaRecomendacao += value.Avaliacao;
                        quantidadeRecomendacao++;
                        
                    });

                    var resumo = {
                        Nome: values[0].Nome,
                        Categoria: values[0].Categoria,
                        Fabricante: values[0].Fabricante,
                        Lojas: lojas,
                        Avaliacao: somatoriaRecomendacao / quantidadeRecomendacao
                    };
                    

                    return resumo;
                }
            ";

            try
            {
                // Filtra por nome do produto
                MapReduceOptions<ProdutoEntity, EntityMapReduce<ResumoProduto>> options = new MapReduceOptions<ProdutoEntity, EntityMapReduce<ResumoProduto>>
                {
                    Filter = Builders<ProdutoEntity>.Filter.Where(p => p.Nome == produto)
                };

                //Chama o método MapReduce do RepositoryBase para executar o MapReduce
                //e retornar no resultado no formato ProdutoLojaPreco
                return MapReduce<ResumoProduto>(funcaoMap, funcaoReduce, options);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao executar MapReduce do ObterResumoProduto");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar MapReduce do ObterResumoProduto");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos.", ex));
            }
        }
    }
}
