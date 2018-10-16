using Arbitragem.Dominio.Exchanges;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Arbitragem.Dominio.Conversores
{
    public class FlowBTCExchangeJsonConverter : JsonConverter<Exchange>
    {
        public override void WriteJson(JsonWriter writer, Exchange value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Exchange ReadJson(JsonReader reader, Type objectType, Exchange existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject) return null;

            var item = JObject.Load(reader);

            if (item["data"] == null) return null;

            var dados = item["data"];

            var precoOfertaAtual = dados["BestOffer"].Value<double>();
            var precoUltimaOfertaEfetivada = dados["LastTradedPx"].Value<double>();
            var precoOfertaMaisAltaDoDia = dados["SessionHigh"].Value<double>();
            var precoOfertaMaisBaixaDoDia = dados["SessionLow"].Value<double>();
            var precoVendaEstimadoPelaExchange = dados["BestOffer"].Value<double>();

            var exchange = new Exchange(Enumeradores.Enumeradores.Exchanges.FlowBTC, precoOfertaAtual,
                precoUltimaOfertaEfetivada, precoOfertaMaisAltaDoDia, precoOfertaMaisBaixaDoDia,
                precoVendaEstimadoPelaExchange);

            return exchange;
        }
    }
}
