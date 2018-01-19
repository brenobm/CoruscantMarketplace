using CoruscantMarketplace.DataLayer.Modelos;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CoruscantMarketplace.DataLayer.Repositorios
{
    public class Repositorio<T>: IRepositorio<T>
        where T: ModeloBase
    {
        private IMongoClient _cliente;
        private IMongoDatabase _db;

        private string GetTypeName()
        {
            return typeof(T).Name;
        }

        public Repositorio()
        {
            _cliente = new MongoClient("mongodb://localhost:27017");
            _db = _cliente.GetDatabase("produtos");
        }

        public void Excluir(ObjectId id)
        {
            _db.GetCollection<T>(GetTypeName()).DeleteOne(t => t.Id == id);
        }

        public T Inserir(T entidade)
        {
            _db.GetCollection<T>(GetTypeName()).InsertOne(entidade);

            return entidade;
        }

        public IEnumerable<T> ListarTodos()
        {
            return _db.GetCollection<T>(GetTypeName()).Find(e => true).ToList();
        }

        public void AtualizarCampos(ObjectId id, object valor)
        {
            var updates = new List<UpdateDefinition<T>>();
            
            foreach(var propriedade in valor.GetType().GetProperties())
            {
                updates.Add(Builders<T>.Update.Set(propriedade.Name, BsonDocumentWrapper.Create(propriedade.PropertyType, propriedade.GetValue(valor))));
            }

            var update = Builders<T>.Update.Combine(updates);

            _db.GetCollection<T>(GetTypeName()).UpdateOne(t => t.Id == id, update);
        }

        public IEnumerable<TRetorno> MapReduce<TRetorno>(string map, string reduce)
        {
            return _db.GetCollection<T>(GetTypeName()).MapReduce<ModeloMapReduce<TRetorno>>(map, reduce).ToList().Select(mp => mp.Value);
        }
    }
}
