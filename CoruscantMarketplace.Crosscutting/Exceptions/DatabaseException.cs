using System;

namespace CoruscantMarketplace.Crosscutting.Exceptions
{
    /// <summary>
    /// Classe para sinalizar uma exceção de conexão com a base de dados
    /// Obrigatório receber a mensagem no construtor
    /// </summary>
    public class DatabaseException : Exception
    {
        /// <summary>
        /// Construtor único da classe
        /// Não possui construtor default
        /// </summary>
        /// <param name="mensagem">
        /// Mensagem de erro que será propagada
        /// </param>
        public DatabaseException(string mensagem) 
            : base(mensagem)
        {

        }
    }
}
