using Arbitragem.Dominio.Exchanges;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arbitragem.Dominio.Conversores
{
    public class BraziliexOrdensJsonConverter : JsonConverter<IEnumerable<Ordem>>
    {

        public override void WriteJson(JsonWriter writer, IEnumerable<Ordem> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Ordem> ReadJson(JsonReader reader, Type objectType, IEnumerable<Ordem> existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject) return Enumerable.Empty<Ordem>();

            var item = JObject.Load(reader);

            if (item == null) return Enumerable.Empty<Ordem>();

            var dadosDeCompraDoResultado = item["bids"];
            var dadosDeVendaDoResultado = item["asks"];

            if (dadosDeCompraDoResultado == null || dadosDeVendaDoResultado == null) return Enumerable.Empty<Ordem>();

            var quantidadeTodasOrdens = dadosDeCompraDoResultado.Count() + dadosDeVendaDoResultado.Count();

            var ordens = new List<Ordem>(quantidadeTodasOrdens);

            foreach (var dadosDeCompra in dadosDeCompraDoResultado)
            {

                var precoDaOrdem = dadosDeCompra["price"].Value<double>();
                var quantidadeDaOrdem = dadosDeCompra["amount"].Value<double>();

                ordens.Add(new Ordem(string.Empty, precoDaOrdem, quantidadeDaOrdem,
                    Enumeradores.Enumeradores.TipoDeOrdem.Compra));
            }

            foreach (var dadosDeVenda in dadosDeVendaDoResultado)
            {
                var precoDaOrdem = dadosDeVenda["price"].Value<double>();
                var quantidadeDaOrdem = dadosDeVenda["amount"].Value<double>();

                ordens.Add(new Ordem(string.Empty, precoDaOrdem, quantidadeDaOrdem,
                    Enumeradores.Enumeradores.TipoDeOrdem.Venda));
            }

            return ordens;
        }
    }
}
