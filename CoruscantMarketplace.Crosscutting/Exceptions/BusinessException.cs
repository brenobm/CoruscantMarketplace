using System;

namespace CoruscantMarketplace.Crosscutting.Exceptions
{
    /// <summary>
    /// Classe para sinalizar uma exceção de negócio
    /// Obrigatório receber a mensagem no construtor
    /// </summary>
    public class BusinessException: Exception
    {
        /// <summary>
        /// Construtor único da classe
        /// Não possui construtor default
        /// </summary>
        /// <param name="mensagem">
        /// Mensagem de erro que será propagada
        /// </param>
        public BusinessException(string mensagem)
            : base(mensagem)
        {

        }
    }
}
