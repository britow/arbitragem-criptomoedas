using System.Net.Http;
using Arbitragem.Dominio.Exchanges.ServicosHttp;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Arbitragem.Console.Extensoes
{
    public static class Http
    {
        public static void AdicionarClientesHttp(this IServiceCollection services)
        {
            services.AddHttpClient<BitcoinTradeServicoHttp>()
                .AddPolicyHandlerFromRegistry((policyRegistry, httpRequestMessage) => 
                    policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>(httpRequestMessage.Method == HttpMethod.Get ? "PoliticaDeRetentativa" : "SemPoliticaOp"));

            services.AddHttpClient<MercadoBitcoinServicoHttp>()
               .AddPolicyHandlerFromRegistry((policyRegistry, httpRequestMessage) =>
                   policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>(httpRequestMessage.Method == HttpMethod.Get ? "PoliticaDeRetentativa" : "SemPoliticaOp"));
        }
    }
}
