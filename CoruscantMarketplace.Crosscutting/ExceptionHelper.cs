using System;

namespace CoruscantMarketplace.Crosscutting
{
    /// <summary>
    /// Classe utilitária para métodos auxiliares referente a exceções
    /// </summary>
    public class ExceptionHelper
    {
        /// <summary>
        /// Cria a string de mensagem de erro para uso em exceções customizadas.
        /// Verifica se é ambiente de desenvolvimento para inserir de StackTrace da exceção original
        /// ou somente a mensagem desta exceção
        /// </summary>
        /// <param name="mensagem">
        /// Mensagem customizada
        /// </param>
        /// <param name="ex">
        /// Exceção original
        /// </param>
        /// <returns></returns>
        public static string FormatarMensagemErro(string mensagem, Exception ex)
        {
            if (Configuracoes.Ambiente == Configuracoes.TipoAmbiente.Desenvolvimento)
            {
                return $"{mensagem} \n {ex.StackTrace}";
            }
            else
            {
                return $"{mensagem} \n {ex.Message}";
            }
        }
    }
}
