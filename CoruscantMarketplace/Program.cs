using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CoruscantMarketplace
{
    /// <summary>
    /// Classe de inicialização da aplicação
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Método de inicialização da aplicação
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Método para a criação do objeto do WebHost
        /// </summary>
        /// <param name="args">
        /// Argumentos
        /// </param>
        /// <returns>
        /// Instância criada do IWebHost
        /// </returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
