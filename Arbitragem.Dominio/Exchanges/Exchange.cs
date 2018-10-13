using System.Collections.Generic;

namespace Arbitragem.Dominio.Exchanges
{
    public class Exchange
    {
        public Enumeradores.Enumeradores.Exchanges Nome { get;}
        public double PrecoOfertaAtual { get; }
        public double PrecoUltimaOfertaEfetivada { get; }
        public double PrecoOfertaMaisAltaDoDia { get; }
        public double PrecoOfertaMaisBaixaDoDia { get;}
        public double PrecoVendaEstimadoPelaExchange { get;  }
        public double Taxas { get; }

        public IReadOnlyList<Ordem> Ordens { get; }

        public Exchange(Enumeradores.Enumeradores.Exchanges nome, double precoOfertaAtual,
            double precoUltimaOfertaEfetivada, double precoOfertaMaisAltaDoDia, double precoOfertaMaisBaixaDoDia,
            double precoVendaEstimadoPelaExchange)
        {
            Nome = nome;
            PrecoOfertaAtual = precoOfertaAtual;
            PrecoUltimaOfertaEfetivada = precoUltimaOfertaEfetivada;
            PrecoOfertaMaisAltaDoDia = precoOfertaMaisAltaDoDia;
            PrecoOfertaMaisBaixaDoDia = precoOfertaMaisBaixaDoDia;
            PrecoVendaEstimadoPelaExchange = precoVendaEstimadoPelaExchange;
        }

    }
}
