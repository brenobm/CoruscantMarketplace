﻿using AutoMapper;
using CoruscantMarketplace.Core.Models;
using CoruscantMarketplace.Crosscutting;
using CoruscantMarketplace.Crosscutting.Exceptions;
using CoruscantMarketplace.DataLayer.Impl.Entities;
using CoruscantMarketplace.DataLayer.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace CoruscantMarketplace.DataLayer.Impl.Repositories
{
    /// <summary>
    /// Implementação da interface que define o contrato dos repositórios
    /// instanciando para o uso do MongoDB para persistência.
    /// Todos os métodos publicos recebem e retornam uma entindade de 
    /// negócio para que as camadas que irá chamar não crie dependência 
    /// com o MongoDB.
    /// A tradução das entidades de negócio para persistência é feita
    /// através do AutoMapper.
    /// </summary>
    /// <typeparam name="T">
    /// Entidade de negócio que o repositório pertence
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// Entitade de persistência equivalente a entidade de negócio
    /// </typeparam>
    public abstract class RepositoryBase<T, TEntity> : IRepositoryBase<T>
        where T: ModelBase
        where TEntity: EntityBase
    {
        /// <summary>
        /// Contém a instância da base de dados do MongoBD
        /// </summary>
        private IMongoDatabase _db;
        
        /// <summary>
        /// Instância do mecanismo de log
        /// </summary>
        protected ILogger<RepositoryBase<T, TEntity>> _logger;

        /// <summary>
        /// Construtor
        /// Responsável por abrir conexão com o banco do MongoDB
        /// </summary>
        /// <param name="logger">
        /// Instancia do mecanismo de logging que será inserido via injeção de dependência
        /// </param>
        public RepositoryBase(ILogger<RepositoryBase<T, TEntity>> logger)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(Configuracoes.MongoConnectionString)
            );

            if (Configuracoes.MongoEnableSSL)
            {
                settings.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = SslProtocols.Tls12
                };
            }

            _db = new MongoClient(settings).GetDatabase(Configuracoes.MongoDatabaseName);
            _logger = logger;
        }

        /// <summary>
        /// Facilita o acesso a coleção do MongoDB de uma entidade específica
        /// </summary>
        /// <returns>
        /// Retorna uma coleção do MongoDB da entidade TEntity
        /// </returns>
        private IMongoCollection<TEntity> GetCollection()
        {
            return _db.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        /// <summary>
        /// Implementação do método AtualizarCampos da interface IRepositoryBase
        /// Utilizando o MongoDB
        /// Método para atualizar um ou mais campos de um objeto
        /// específico da entidade
        /// </summary>
        /// <param name="id">
        /// Identificador do objeto
        /// </param>
        /// <param name="valor">
        /// Objeto anônimo contendo o(s) campo(s) e seus respectivo(s) valor(es).
        /// Ex: new { Campo1: "Valor1", Campo2: 2 }
        /// </param>
        public void AtualizarCampos(string id, object valor)
        {
            try
            {
                ObjectId objectId = new ObjectId(id);

                var updates = new List<UpdateDefinition<TEntity>>();

                foreach (var propriedade in valor.GetType().GetProperties())
                {
                    updates.Add(Builders<TEntity>.Update.Set(propriedade.Name, BsonDocumentWrapper.Create(propriedade.PropertyType, propriedade.GetValue(valor))));
                }

                var update = Builders<TEntity>.Update.Combine(updates);

                GetCollection().UpdateOne(t => t.Id == objectId, update);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao atualizar campos");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar atualizar o objeto na base de dados.", mex));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar campos");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar atualizar o objeto.", ex));
            }
        }

        /// <summary>
        /// Implementação do método Excluir da interface IRepositoryBase
        /// Utilizando o MongoDB
        /// Método que exclui um objeto específico da entidade T
        /// </summary>
        /// <param name="id">
        /// Identificador do objeto
        /// </param>
        public void Excluir(string id)
        {
            try
            {
                ObjectId objectId = new ObjectId(id);
                GetCollection().DeleteOne(t => t.Id == objectId);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao excluir");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar excluir o objeto na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar excluir o objeto.", ex));
            }
        }

        /// <summary>
        /// Implementação do método Inserir da interface IRepositoryBase
        /// Utilizando o MongoDB
        /// Método para inserir um objeto da entidade T
        /// </summary>
        /// <param name="entidade">
        /// Objeto que será persistido
        /// </param>
        /// <returns>
        /// Retorna o objeto persistido com o Id preenchido
        /// </returns>
        public T Inserir(T entidade)
        {
            try
            {
                TEntity entidadeBanco = Mapper.Map<TEntity>(entidade);

                GetCollection().InsertOne(entidadeBanco);

                return entidade;
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao inserir");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar inserir o objeto na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar inserir o objeto.", ex));
            }
        }

        /// <summary>
        /// Implementação do método ListarTodos da interface IRepositoryBase
        /// Utilizando o MongoDB
        /// Método para listar todos os objetos existentes da entidade T
        /// </summary>
        /// <returns>
        /// Retorna a lista de todos os objetos existentes
        /// </returns>
        public IEnumerable<T> ListarTodos()
        {
            try
            {
                IEnumerable<TEntity> entidades = GetCollection().Find(e => true).ToEnumerable();
                return Mapper.Map<IEnumerable<T>>(entidades);
            }
            catch (MongoException mex)
            {
                _logger.LogError(mex, "Erro ao listar");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos na base de dados.", mex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar");

                throw new DatabaseException(ExceptionHelper.FormatarMensagemErro("Erro ao tentar listar os objetos.", ex));
            }
        }

        /// <summary>
        /// Método para execução genérica de MapReduce no MongoDB
        /// Para que possa ser utilizado pela implementação dos
        /// Repositories concretos
        /// </summary>
        /// <typeparam name="TReturn">
        /// Tipo da Entidade de Negócio que será retornada
        /// A mesma será a entidade que o MapReduce irá guardar na propriedade value
        /// </typeparam>
        /// <param name="map">
        /// String contendo a função Javascript para o Map
        /// </param>
        /// <param name="reduce">
        /// String contendo a função Javascript para o Reduce
        /// </param>
        /// <param name="options">
        /// Opções do MapReduce
        /// </param>
        /// <returns>
        /// Coleção de elementos do tipo TReturn recuperado do processamento
        /// do MapReduce.
        /// É retornado somente o valor da propriedasde value da entidade
        /// gerada na execução do MapReduce
        /// </returns>
        protected IEnumerable<TReturn> MapReduce<TReturn>(string map, string reduce, MapReduceOptions<TEntity, EntityMapReduce<TReturn>> options = null)
        {
            return GetCollection().MapReduce<EntityMapReduce<TReturn>>(map, reduce, options).ToList().Select(mp => mp.Value);
        }
    }
}
