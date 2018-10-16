using System;
using System.Collections.Generic;
using System.Linq;
using Arbitragem.Dominio.Exchanges;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Arbitragem.Dominio.Conversores
{
    public class FlowBTCOrdensJsonConverter : JsonConverter<IEnumerable<Ordem>>
    {

        public override void WriteJson(JsonWriter writer, IEnumerable<Ordem> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Ordem> ReadJson(JsonReader reader, Type objectType, IEnumerable<Ordem> existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject) return null;

            var item = JObject.Load(reader);

            if (item["data"] == null) return null;

            var dados = item["data"];

            var dadosDeCompraDoResultado = dados["bids"];
            var dadosDeVendaDoResultado = dados["asks"];

            if (dadosDeCompraDoResultado == null || dadosDeVendaDoResultado == null) return null;

            var quantidadeTodasOrdens = dadosDeCompraDoResultado.Count() + dadosDeVendaDoResultado.Count();

            var ordens = new List<Ordem>(quantidadeTodasOrdens);

            foreach (var dadosDeCompra in dadosDeCompraDoResultado)
            {

                var precoDaOrdem = dadosDeCompra["Price"].Value<double>();
                var quantidadeDaOrdem = dadosDeCompra["Quantity"].Value<double>();

                ordens.Add(new Ordem(string.Empty, precoDaOrdem, quantidadeDaOrdem,
                    Enumeradores.Enumeradores.TipoDeOrdem.Compra));
            }

            foreach (var dadosDeVenda in dadosDeVendaDoResultado)
            {
                var precoDaOrdem = dadosDeVenda["Price"].Value<double>();
                var quantidadeDaOrdem = dadosDeVenda["Quantity"].Value<double>();

                ordens.Add(new Ordem(string.Empty, precoDaOrdem, quantidadeDaOrdem,
                    Enumeradores.Enumeradores.TipoDeOrdem.Venda));
            }

            return ordens;
        }
    }
}
