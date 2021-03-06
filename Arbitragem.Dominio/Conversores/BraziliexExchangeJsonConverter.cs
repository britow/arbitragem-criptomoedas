﻿using Arbitragem.Dominio.Exchanges;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Arbitragem.Dominio.Conversores
{
    public class BraziliexExchangeJsonConverter : JsonConverter<Exchange>
    {
        public override void WriteJson(JsonWriter writer, Exchange value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Exchange ReadJson(JsonReader reader, Type objectType, Exchange existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject) return null;

            var dados = JObject.Load(reader);

            if (dados == null) return null;

            var precoOfertaAtual = dados["lowestAsk"].Value<double>();
            var precoUltimaOfertaEfetivada = dados["last"].Value<double>();
            var precoOfertaMaisAltaDoDia = dados["highestBid24"].Value<double>();
            var precoOfertaMaisBaixaDoDia = dados["lowestAsk24"].Value<double>();
            var precoVendaEstimadoPelaExchange = dados["lowestAsk"].Value<double>();

            var exchange = new Exchange(Enumeradores.Enumeradores.Exchanges.Braziliex, precoOfertaAtual,
                precoUltimaOfertaEfetivada, precoOfertaMaisAltaDoDia, precoOfertaMaisBaixaDoDia,
                precoVendaEstimadoPelaExchange);

            return exchange;
        }
    }
}
