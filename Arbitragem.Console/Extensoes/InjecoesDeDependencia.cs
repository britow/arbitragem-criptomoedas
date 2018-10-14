using Arbitragem.Dominio;
using Arbitragem.Dominio.Exchanges.Construtores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Arbitragem.Console.Extensoes
{
    public static class InjecoesDeDependencia
    {
        public static void AdicionarInjecoesDeDependencia(this IServiceCollection services)
        {
            services.AddScoped<ConstrutorExchangeBitcoinTrade>();
            services.AddScoped<ConstrutorExchangeMercadoBitcoin>();

            services.AddScoped<FabricaDeContrutoresDeExchanges>();
            services.AddScoped<IConstrutorGeralDeExchanges, ConstrutorGeralDeExchanges>();
            services.AddScoped<IServicoDeArbitragem, ServicoDeArbitragem>();

            services.AddSingleton<IHostedService, ProcessoDeArbitragem>();

        }
    }
}
