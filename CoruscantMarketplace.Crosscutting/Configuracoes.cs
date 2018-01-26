namespace CoruscantMarketplace.Crosscutting
{
    /// <summary>
    /// Classe para abstrair as configurações no appsettings
    /// Propriedades estáticas para visibilidade em todos os projetos
    /// sem a necessidade de acoplamento com o componente
    /// Microsoft.Extensions.Options
    /// </summary>
    public class Configuracoes
    {
        /// <summary>
        /// String de conexão do MongoDB
        /// </summary>
        public static string MongoConnectionString { get; set; }

        /// <summary>
        /// Nome da Base de dados do MongoDB
        /// </summary>
        public static string MongoDatabaseName { get; set; }

        /// <summary>
        /// Enable SSL on MongoDB
        /// </summary>
        public static bool MongoEnableSSL { get; set; }
        /// <summary>
        /// Tipo de ambiente no qual a aplicação está rodadno
        /// </summary>
        public static TipoAmbiente Ambiente { get; set; }

        /// <summary>
        /// Usuário da API Auth0
        /// </summary>
        public static string Auth0User { get; set; }

        /// <summary>
        /// Senha do usuário da API Auth0
        /// </summary>
        public static string Auth0Password { get; set; }

        /// <summary>
        /// ClientId da API Auth0
        /// </summary>
        public static string Auth0ClientId { get; set; }

        /// <summary>
        /// ClientSecret da API Auth0
        /// </summary>
        public static string Auth0ClientSecret { get; set; }

        /// <summary>
        /// Audience da API Auth0
        /// </summary>
        public static string Auth0Audience { get; set; }

        /// <summary>
        ///  GrantType da API Auth0
        /// </summary>
        public static string Auth0GrantType { get; set; }

        /// <summary>
        /// Authority da API Auth0
        /// </summary>
        public static string Auth0Authority { get; set; }

        /// <summary>
        /// Enumerator que define os tipos de ambientes
        /// </summary>
        public enum TipoAmbiente
        {
            Desenvolvimento,
            Producao
        }

    }
}
