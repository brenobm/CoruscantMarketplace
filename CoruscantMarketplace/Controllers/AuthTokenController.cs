using CoruscantMarketplace.Core.Business;
using Microsoft.AspNetCore.Mvc;

namespace CoruscantMarketplace.Controllers
{
    /// <summary>
    /// API para geração de Token para teste da APi Produto
    /// </summary>
    [Produces("application/json")]
    [Route("api/AuthToken")]
    public class AuthTokenController : Controller
    {
        /// <summary>
        /// Classe que regra de negócio da geração do Token
        /// </summary>
        private IAuthTokenBusiness _authTokenBusiness;

        /// <summary>
        /// Construtor que terá uma instância da IAuthTokenBusiness entregue via injeção de pendencia.
        /// </summary>
        /// <param name="authTokenBusiness">
        /// Classe que regra de negócio da geração do Token
        /// </param>
        public AuthTokenController(IAuthTokenBusiness authTokenBusiness)
        {
            _authTokenBusiness = authTokenBusiness;
        }

        /// <summary>
        /// Gera o Token necessário para chamar as APIs do Produto.
        /// Este Token deve ser passado no Header da chamada como
        /// "authorization" : "Bearer TOKEN_GERADO"
        /// </summary>
        /// <returns>
        /// Token de autenticação
        /// </returns>
        [HttpGet]
        public string GetToken()
        {
            return _authTokenBusiness.GerarToken();
        }
    }
}