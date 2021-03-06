﻿using Arbitragem.Dominio.Conversores;
using Arbitragem.Dominio.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Arbitragem.Dominio.Exchanges.ServicosHttp
{
    public class MercadoBitcoinServicoHttp
    {
        private readonly HttpClient _clienteHttp;

        public MercadoBitcoinServicoHttp(HttpClient clienteHttp, IConfiguration configuration)
        {
            var uri = configuration[$"{Enumeradores.Enumeradores.Exchanges.MercadoBitcoin}:EnderecoBase"];

            if (string.IsNullOrWhiteSpace(uri))
                throw new ExcecaoArbitragem($"EnderecoBase não foi encontrado para a Exchange {Enumeradores.Enumeradores.Exchanges.MercadoBitcoin}");

            clienteHttp.BaseAddress = new Uri(uri);
            clienteHttp.DefaultRequestHeaders.Add("Accept", "application/json");

            _clienteHttp = clienteHttp;
        }

        public async Task<Exchange> ObterInformacoesDaExchange()
        {
            var resposta = await _clienteHttp.GetAsync("/api/BTC/ticker");

            resposta.EnsureSuccessStatusCode();

            var resultadoEmString = await resposta.Content
                .ReadAsStringAsync();

            var exchange = JsonConvert.DeserializeObject<Exchange>(resultadoEmString,
                FabricaDeConversoresJson.CriaConversorJsonDeExchange(Enumeradores.Enumeradores.Exchanges.MercadoBitcoin));

            return exchange;
        }


        public async Task<IEnumerable<Ordem>> ObterOrdensDaExchange()
        {
            var resposta = await _clienteHttp.GetAsync("/api/BTC/orderbook");

            resposta.EnsureSuccessStatusCode();

            var resultadoEmString = await resposta.Content
                .ReadAsStringAsync();

            var ordens = JsonConvert.DeserializeObject<IEnumerable<Ordem>>(resultadoEmString,
                FabricaDeConversoresJson.CriaConversorJsonDeOrdens(Enumeradores.Enumeradores.Exchanges.MercadoBitcoin));

            return ordens;
        }
    }
}
