using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Arbitragem.Dominio.Conversores;
using Arbitragem.Dominio.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Arbitragem.Dominio.Exchanges.ServicosHttp
{
    public class FlowBTCServicoHttp
    {
        private readonly HttpClient _clienteHttp;

        public FlowBTCServicoHttp(HttpClient clienteHttp, IConfiguration configuration)
        {
            var uri = configuration[$"{Enumeradores.Enumeradores.Exchanges.FlowBTC}:EnderecoBase"];

            if (string.IsNullOrWhiteSpace(uri))
                throw new ExcecaoArbitragem($"EnderecoBase não foi encontrado para a Exchange {Enumeradores.Enumeradores.Exchanges.FlowBTC}");

            clienteHttp.BaseAddress = new Uri(uri);
            clienteHttp.DefaultRequestHeaders.Add("Accept", "application/json");

            _clienteHttp = clienteHttp;
        }

        public async Task<Exchange> ObterInformacoesDaExchange()
        {
            var resposta = await _clienteHttp.GetAsync("/v1/ticker/BTC");

            resposta.EnsureSuccessStatusCode();

            var resultadoEmString = await resposta.Content
                .ReadAsStringAsync();

            var exchange = JsonConvert.DeserializeObject<Exchange>(resultadoEmString,
                new FlowBTCExchangeJsonConverter());

            return exchange;
        }


        public async Task<IEnumerable<Ordem>> ObterOrdensDaExchange()
        {
            var resposta = await _clienteHttp.GetAsync("/v1/book/BTC");

            resposta.EnsureSuccessStatusCode();

            var resultadoEmString = await resposta.Content
                .ReadAsStringAsync();

            var ordens = JsonConvert.DeserializeObject<IEnumerable<Ordem>>(resultadoEmString,
                new FlowBTCOrdensJsonConverter());

            return ordens;
        }
    }
}
