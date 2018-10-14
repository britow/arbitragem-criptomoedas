using Arbitragem.Dominio.Exchanges;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Arbitragem.Dominio.Conversores
{
    public class MercadoBitcoinConversor
    {
        public static Exchange ConverterDadosDaExchange(dynamic dadosDinamicos)
        {
            const string cultura = "pt-br";

            double.TryParse(Convert.ToString(dadosDinamicos?.buy),
               NumberStyles.Currency,
               new CultureInfo(cultura),
               out double precoOfertaAtual);

            double.TryParse(Convert.ToString(dadosDinamicos?.last),
              NumberStyles.Currency,
              new CultureInfo(cultura),
              out double precoUltimaOfertaEfetivada);

            double.TryParse(Convert.ToString(dadosDinamicos?.high),
             NumberStyles.Currency,
             new CultureInfo(cultura),
             out double precoOfertaMaisAltaDoDia);

            double.TryParse(Convert.ToString(dadosDinamicos?.low),
             NumberStyles.Currency,
             new CultureInfo(cultura),
             out double precoOfertaMaisBaixaDoDia);

            double.TryParse(Convert.ToString(dadosDinamicos?.sell),
              NumberStyles.Currency,
              new CultureInfo(cultura),
              out double precoVendaEstimadoPelaExchange);

            var exchange = new Exchange(Enumeradores.Enumeradores.Exchanges.MercadoBitcoin, precoOfertaAtual,
                precoUltimaOfertaEfetivada, precoOfertaMaisAltaDoDia, precoOfertaMaisBaixaDoDia,
                precoVendaEstimadoPelaExchange);

            return exchange;
        }

        public static IEnumerable<Ordem> ConverterOrdensDaExchange(dynamic dadosDinamicos)
        {
            var dadosDeCompraDoResultado = dadosDinamicos?.bids;
            var dadosDeVendaDoResultado = dadosDinamicos?.asks;

            const string cultura = "pt-br";

            foreach (var resultado in dadosDeCompraDoResultado)
            {

                double.TryParse(Convert.ToString(resultado[0]),
                   NumberStyles.Currency,
                   new CultureInfo(cultura),
                   out double precoDaOrdem);

                double.TryParse(Convert.ToString(resultado[1]),
                   NumberStyles.Any,
                   new CultureInfo(cultura),
                   out double quantidadeDaOrdem);

                yield return new Ordem(string.Empty, precoDaOrdem, quantidadeDaOrdem,
                    Enumeradores.Enumeradores.TipoDeOrdem.Compra);
            }

            foreach (var resultado in dadosDeVendaDoResultado)
            {

                double.TryParse(Convert.ToString(resultado[0]),
                   NumberStyles.Currency,
                   new CultureInfo(cultura),
                   out double precoDaOrdem);

                double.TryParse(Convert.ToString(resultado[1]),
                   NumberStyles.Any,
                   new CultureInfo(cultura),
                   out double quantidadeDaOrdem);

                yield return new Ordem(string.Empty, precoDaOrdem, quantidadeDaOrdem,
                    Enumeradores.Enumeradores.TipoDeOrdem.Venda);
            }
        }
    }
}
