using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
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

            var exchange = CriarExchangeComOsResultadosObtidosDaChamadaHttp(dadosDoResultado);

            return exchange;
        }

        private static Exchange CriarExchangeComOsResultadosObtidosDaChamadaHttp(dynamic resultadoDaChamadaHttp)
        {
            const string cultura = "pt-br";

            double.TryParse(Convert.ToString(resultadoDaChamadaHttp?.buy),
               NumberStyles.Currency,
               new CultureInfo(cultura),
               out double precoOfertaAtual);

            double.TryParse(Convert.ToString(resultadoDaChamadaHttp?.last),
              NumberStyles.Currency,
              new CultureInfo(cultura),
              out double precoUltimaOfertaEfetivada);

            double.TryParse(Convert.ToString(resultadoDaChamadaHttp?.high),
             NumberStyles.Currency,
             new CultureInfo(cultura),
             out double precoOfertaMaisAltaDoDia);

            double.TryParse(Convert.ToString(resultadoDaChamadaHttp?.low),
             NumberStyles.Currency,
             new CultureInfo(cultura),
             out double precoOfertaMaisBaixaDoDia);

            double.TryParse(Convert.ToString(resultadoDaChamadaHttp?.sell),
              NumberStyles.Currency,
              new CultureInfo(cultura),
              out double precoVendaEstimadoPelaExchange);

            var exchange = new Exchange(Enumeradores.Enumeradores.Exchanges.BitcoinTrade, precoOfertaAtual,
                precoUltimaOfertaEfetivada, precoOfertaMaisAltaDoDia, precoOfertaMaisBaixaDoDia,
                precoVendaEstimadoPelaExchange);


            return exchange;
        }
    }
}
