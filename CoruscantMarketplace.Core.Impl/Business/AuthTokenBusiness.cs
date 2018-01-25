using CoruscantMarketplace.Core.Business;
using CoruscantMarketplace.Crosscutting;
using CoruscantMarketplace.Crosscutting.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace CoruscantMarketplace.Core.Impl.Business
{

    /// <summary>
    /// Implementação da interface para a entidade de negócio que irá gerar um Token Auth0 válido para
    /// o testes das APIs
    /// </summary>
    public class AuthTokenBusiness : IAuthTokenBusiness
    {
        /// <summary>
        /// Instância do mecanismo de log
        /// </summary>
        private ILogger<AuthTokenBusiness> _logger;

        /// <summary>
        /// Construtor para injeção de dependência
        /// </summary>
        /// <param name="logger">
        /// Instancia do mecanismo de logging que será inserido via injeção de dependência
        /// </param>
        public AuthTokenBusiness(ILogger<AuthTokenBusiness> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Acessa a API do OAuth0 para gerar o token válido para a aplicação
        /// É necessário passar as configurações configurada na conta do Auth0
        /// </summary>
        /// <returns>
        /// Token Válido
        /// </returns>
        public string GerarToken()
        {
            try
            {
                var client = new RestClient("https://coruscantmarketplace.auth0.com/oauth/token");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");

                var requisicao = new
                {
                    client_id = Configuracoes.Auth0ClientId,
                    client_secret = Configuracoes.Auth0ClientSecret,
                    audience = Configuracoes.Auth0Audience,
                    grant_type = Configuracoes.Auth0GrantType
                };

                request.AddParameter("application/json", JsonConvert.SerializeObject(requisicao), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                var retorno = JsonConvert.DeserializeAnonymousType(response.Content, new { access_token = "", expires_in = 0, token_type = "" });

                return retorno.access_token;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o Token");

                throw new BusinessException(ExceptionHelper.FormatarMensagemErro("Erro ao gerar o novo Token", ex));
            }
        }
    }
}
