using Arbitragem.Console.Extensoes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Arbitragem.Console
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await Executar();
        }

        private static async Task Executar()
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("configuration.json", true, true);

                })
                .ConfigureServices((hostContext, services) =>
                {

                    services.AdicionarPoliticas();

                    services.AdicionarClientesHttp();

                    services.AdicionarInjecoesDeDependencia();
                });
            
            await builder.RunConsoleAsync();
        }
    }
}
