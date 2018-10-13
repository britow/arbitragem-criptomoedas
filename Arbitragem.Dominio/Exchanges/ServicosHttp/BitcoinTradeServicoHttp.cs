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
    public class BitcoinTradeServicoHttp
    {
        private readonly HttpClient _clienteHttp;

        public BitcoinTradeServicoHttp(HttpClient clienteHttp, IConfiguration configuration)
        {
            var uri = configuration[$"{Enumeradores.Enumeradores.Exchanges.BitcoinTrade}:EnderecoBase"];

            if (string.IsNullOrWhiteSpace(uri))
                throw new ExcecaoArbitragem($"EnderecoBase não foi encontrado para a Exchange {Enumeradores.Enumeradores.Exchanges.BitcoinTrade}");

            clienteHttp.BaseAddress = new Uri(uri);
            clienteHttp.DefaultRequestHeaders.Add("Accept", "application/json");

            _clienteHttp = clienteHttp;
        }

        public async Task<Exchange> ObterInformacoesDaExchange()
        {
            var resposta = await _clienteHttp.GetAsync("/v1/public/BTC/ticker");

            resposta.EnsureSuccessStatusCode();

            var resultadoEmString = await resposta.Content
                .ReadAsStringAsync();

            dynamic resultadoDinamico = JsonConvert.DeserializeObject(resultadoEmString);

            var dadosDoResultado = resultadoDinamico?.data;

            var exchange = BitcoinTradeConversor.ConverterDadosDaExchange(dadosDoResultado);

            return exchange;
        }


        public async Task<IEnumerable<Ordem>> ObterOrdensDaExchange()
        {
            var resposta = await _clienteHttp.GetAsync("/v1/public/BTC/orders");

            resposta.EnsureSuccessStatusCode();

            var resultadoEmString = await resposta.Content
                .ReadAsStringAsync();

            dynamic resultadoDinamico = JsonConvert.DeserializeObject(resultadoEmString);

            var dadosDoResultado = resultadoDinamico?.data;
           
            var ordens = BitcoinTradeConversor.ConverterOrdensDaExchange(dadosDoResultado);

            return ordens;
        }

    }
}
