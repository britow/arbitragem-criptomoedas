using Arbitragem.Dominio.Exchanges;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Arbitragem.Dominio.Conversores
{
    public class BitcoinTradeExchangeJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Exchange).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);

                if (item["data"] != null)
                {
                    var dados = item["data"];

                    double precoOfertaAtual = dados["buy"].Value<double>();
                    double precoUltimaOfertaEfetivada = dados["last"].Value<double>();
                    double precoOfertaMaisAltaDoDia = dados["high"].Value<double>();
                    double precoOfertaMaisBaixaDoDia = dados["low"].Value<double>();
                    double precoVendaEstimadoPelaExchange = dados["sell"].Value<double>();

                    var exchange = new Exchange(Enumeradores.Enumeradores.Exchanges.BitcoinTrade, precoOfertaAtual,
                        precoUltimaOfertaEfetivada, precoOfertaMaisAltaDoDia, precoOfertaMaisBaixaDoDia,
                        precoVendaEstimadoPelaExchange);

                    return exchange;
                }
            }

            return null;

        }
    }
}
