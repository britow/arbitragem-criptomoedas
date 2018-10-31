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
    public class BitCambioServicoHttp
    {
        private readonly HttpClient _clienteHttp;

        public BitCambioServicoHttp(HttpClient clienteHttp, IConfiguration configuration)
        {
            var uri = configuration[$"{Enumeradores.Enumeradores.Exchanges.BitCambio}:EnderecoBase"];

            if (string.IsNullOrWhiteSpace(uri))
                throw new ExcecaoArbitragem($"EnderecoBase não foi encontrado para a Exchange {Enumeradores.Enumeradores.Exchanges.BitCambio}");

            clienteHttp.BaseAddress = new Uri(uri);
            clienteHttp.DefaultRequestHeaders.Add("Accept", "application/json");

            _clienteHttp = clienteHttp;
        }

        public async Task<Exchange> ObterInformacoesDaExchange()
        {
            var resposta = await _clienteHttp.GetAsync("/api/v1/BRL/ticker?crypto_currency=BTC");

            resposta.EnsureSuccessStatusCode();

            var resultadoEmString = await resposta.Content
                .ReadAsStringAsync();

            var exchange = JsonConvert.DeserializeObject<Exchange>(resultadoEmString,
                FabricaDeConversoresJson.CriaConversorJsonDeExchange(Enumeradores.Enumeradores.Exchanges.BitCambio));

            return exchange;
        }


        public async Task<IEnumerable<Ordem>> ObterOrdensDaExchange()
        {
            var resposta = await _clienteHttp.GetAsync("/api/v1/BRL/orderbook?crypto_currency=BTC");

            resposta.EnsureSuccessStatusCode();

            var resultadoEmString = await resposta.Content
                .ReadAsStringAsync();

            var ordens = JsonConvert.DeserializeObject<IEnumerable<Ordem>>(resultadoEmString,
                FabricaDeConversoresJson.CriaConversorJsonDeOrdens(Enumeradores.Enumeradores.Exchanges.BitCambio));

            return ordens;
        }
    }
}
