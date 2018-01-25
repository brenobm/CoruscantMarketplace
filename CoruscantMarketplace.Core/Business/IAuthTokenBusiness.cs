using System;
using System.Collections.Generic;
using System.Text;

namespace CoruscantMarketplace.Core.Business
{
    /// <summary>
    /// Interface para a entidade de negócio que irá gerar um Token Auth0 válido para
    /// o testes das APIs
    /// </summary>
    public interface IAuthTokenBusiness
    {
        /// <summary>
        /// Gera um Token válido para acessar as APIs de Produto
        /// </summary>
        /// <returns>
        /// Token Válido
        /// </returns>
        string GerarToken();
    }
}
