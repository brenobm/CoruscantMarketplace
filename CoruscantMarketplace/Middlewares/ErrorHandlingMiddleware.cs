using CoruscantMarketplace.Crosscutting.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CoruscantMarketplace.Middlewares
{
    /// <summary>
    /// Middleware para tratamento de exceções
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// Delegate para tratar a requisição em processamento
        /// </summary>
        private readonly RequestDelegate _requisicao;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="requisicao">
        /// Recebe o delegate de tratamento da requisição
        /// </param>
        public ErrorHandlingMiddleware(RequestDelegate requisicao)
        {
            this._requisicao = requisicao;
        }

        /// <summary>
        /// Método de invocação do Middleware
        /// Irá verificar se a execução da requisição se dá com sucesso
        /// caso contrário será feito o tratamento da exceção
        /// </summary>
        /// <param name="contexto">
        /// Contexto da requisição
        /// </param>
        /// <returns>
        /// Por se um método asincrono, seu retorno é um Task
        /// </returns>
        public async Task Invoke(HttpContext contexto)
        {
            try
            {
                await _requisicao(contexto);
            }
            catch (Exception ex)
            {
                await TrataExcecao(contexto, ex);
            }
        }

        /// <summary>
        /// Método de tratamento de exceção na requisição processada.
        /// Ele irá avaliar o tipo de exceção para poder retornar o código
        /// HTTP correspondente retornar um objeto com a mensagem de erro
        /// para o solicitante da Web.API
        /// </summary>
        /// <param name="contexto">
        /// Contexto da requisição
        /// </param>
        /// <param name="excecao">
        /// Exceção que foi lançada no processamento da requisição
        /// </param>
        /// <returns>
        /// Por se um método asincrono, seu retorno é um Task
        /// </returns>
        private static Task TrataExcecao(HttpContext contexto, Exception excecao)
        {
            // Se o tipo de exceção não for uma definida pela aplicação
            // será um InternalServerError (exceção não esperada)
            HttpStatusCode codigo = HttpStatusCode.InternalServerError;

            //if (excecao is MyNotFoundException) codigo = HttpStatusCode.NotFound;
            //else if (excecao is MyUnauthorizedException) codigo = HttpStatusCode.Unauthorized;
            //else 

            // Caso seja uma BusinessException ou DatabaseException
            // irá retornar Bad Request
            if (excecao is BusinessException 
                || excecao is DatabaseException)
            {
                codigo = HttpStatusCode.BadRequest;
            }
            // Se for uma UnauthorizedException irá retornar um
            // Unauthorized
            else if (excecao is UnauthorizedException)
            {
                codigo = HttpStatusCode.Unauthorized;
            }

            var result = JsonConvert.SerializeObject( new
                {
                    error = excecao.Message
                });

            contexto.Response.ContentType = "application/json";
            contexto.Response.StatusCode = (int)codigo;

            // Grava no response o conteúdo do objeto com a mensagem de erro para ser retornado 
            // ao solicitante
            return contexto.Response.WriteAsync(result);
        }

    }
}
