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
    }
}
