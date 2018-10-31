using System.Collections.Generic;
using Arbitragem.Dominio.Exchanges;
using Newtonsoft.Json;

namespace Arbitragem.Dominio.Conversores
{
    public sealed class FabricaDeConversoresJson
    {
        public static JsonConverter<Exchange> CriaConversorJsonDeExchange(Enumeradores.Enumeradores.Exchanges exchanges)
        {
            switch (exchanges)
            {
                case Enumeradores.Enumeradores.Exchanges.BitcoinTrade:
                    return new BitcoinTradeExchangeJsonConverter();
                case Enumeradores.Enumeradores.Exchanges.BitCambio:
                    return new BitCambioExchangeJsonConverter();
                case Enumeradores.Enumeradores.Exchanges.Braziliex:
                    return new BraziliexExchangeJsonConverter();
                case Enumeradores.Enumeradores.Exchanges.MercadoBitcoin:
                    return new MercadoBitcoinExchangeJsonConverter();
                case Enumeradores.Enumeradores.Exchanges.FlowBTC:
                    return new FlowBTCExchangeJsonConverter();
                default:
                    return new BitcoinTradeExchangeJsonConverter();
            }
        }

        public static JsonConverter<IEnumerable<Ordem>> CriaConversorJsonDeOrdens(Enumeradores.Enumeradores.Exchanges exchanges)
        {
            switch (exchanges)
            {
                case Enumeradores.Enumeradores.Exchanges.BitcoinTrade:
                    return new BitcoinTradeOrdensJsonConverter();
                case Enumeradores.Enumeradores.Exchanges.BitCambio:
                    return new BitCambioOrdensJsonConverter();
                case Enumeradores.Enumeradores.Exchanges.Braziliex:
                    return new BraziliexOrdensJsonConverter();
                case Enumeradores.Enumeradores.Exchanges.MercadoBitcoin:
                    return new MercadoBitcoinOrdensJsonConverter();
                case Enumeradores.Enumeradores.Exchanges.FlowBTC:
                    return new FlowBTCOrdensJsonConverter();
                default:
                    return new BitcoinTradeOrdensJsonConverter();
            }
        }
    }
}
