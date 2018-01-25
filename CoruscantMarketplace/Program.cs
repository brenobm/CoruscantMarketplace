using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

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
        /// Método para a criação do objeto do WebHost para usar o Kestrel
        /// </summary>
        /// <param name="args">
        /// Argumentos
        /// </param>
        /// <returns>
        /// Instância criada do IWebHost
        /// </returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();

            var host = WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            return host;
        }
    }
}
