using CoruscantMarketplace.Core.Models;
using System.Collections.Generic;

namespace CoruscantMarketplace.DataLayer.Repositories
{
    /// <summary>
    /// Interface que define o contrato dos repositórios
    /// independente do mecanismo de persistência
    /// </summary>
    /// <typeparam name="T">
    /// Entidade de negócio que o repositório pertence
    /// </typeparam>
    public interface IRepositoryBase<T>
        where T: ModelBase
    {
        /// <summary>
        /// Método para inserir um objeto da entidade T
        /// </summary>
        /// <param name="entidade">
        /// Objeto que será persistido
        /// </param>
        /// <returns>
        /// Retorna o objeto persistido com o Id preenchido
        /// </returns>
        T Inserir(T entidade);

        /// <summary>
        /// Método para listar todos os objetos existentes da entidade T
        /// </summary>
        /// <returns>
        /// Retorna a lista de todos os objetos existentes
        /// </returns>
        IEnumerable<T> ListarTodos();

        /// <summary>
        /// Método que exclui um objeto específico da entidade T
        /// </summary>
        /// <param name="id">
        /// Identificador do objeto
        /// </param>
        void Excluir(string id);

        /// <summary>
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
        void AtualizarCampos(string id, object valor);
    }
}
