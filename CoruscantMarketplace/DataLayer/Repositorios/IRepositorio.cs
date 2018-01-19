using MongoDB.Bson;
using System.Collections.Generic;

namespace CoruscantMarketplace.DataLayer.Repositorios
{
    public interface IRepositorio<T>
    {
        T Inserir(T entidade);
        IEnumerable<T> ListarTodos();
        void Excluir(ObjectId id);
        void AtualizarCampos(ObjectId id, object valor);
        IEnumerable<TRetorno> MapReduce<TRetorno>(string map, string reduce);
    }
}