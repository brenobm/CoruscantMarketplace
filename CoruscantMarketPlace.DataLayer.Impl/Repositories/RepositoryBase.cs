using AutoMapper;
using CoruscantMarketplace.Core.Models;
using CoruscantMarketplace.Crosscutting;
using CoruscantMarketplace.DataLayer.Impl.Entities;
using CoruscantMarketplace.DataLayer.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

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
        /// Construtor
        /// Responsável por abrir conexão com o banco do MongoDB
        /// </summary>
        public RepositoryBase()
        {
            _db = new MongoClient(Configuracoes.MongoConnectionString).GetDatabase(Configuracoes.MongoDatabaseName);
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
            ObjectId objectId = new ObjectId(id);

            var updates = new List<UpdateDefinition<TEntity>>();

            foreach (var propriedade in valor.GetType().GetProperties())
            {
                updates.Add(Builders<TEntity>.Update.Set(propriedade.Name, BsonDocumentWrapper.Create(propriedade.PropertyType, propriedade.GetValue(valor))));
            }

            var update = Builders<TEntity>.Update.Combine(updates);

            GetCollection().UpdateOne(t => t.Id == objectId, update);
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
            ObjectId objectId = new ObjectId(id);
            GetCollection().DeleteOne(t => t.Id == objectId);
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
            TEntity entidadeBanco = Mapper.Map<TEntity>(entidade);

            GetCollection().InsertOne(entidadeBanco);

            return entidade;
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
            return Mapper.Map<IEnumerable<T>>(GetCollection().Find(e => true).ToEnumerable());
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
        /// <returns>
        /// Coleção de elementos do tipo TReturn recuperado do processamento
        /// do MapReduce.
        /// É retornado somente o valor da propriedasde value da entidade
        /// gerada na execução do MapReduce
        /// </returns>
        protected IEnumerable<TReturn> MapReduce<TReturn>(string map, string reduce)
        {
            return GetCollection().MapReduce<EntityMapReduce<TReturn>>(map, reduce).ToList().Select(mp => mp.Value);
        }
    }
}
