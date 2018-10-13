using System.Collections.Generic;
using System.Linq;

namespace Arbitragem.Dominio.Exchanges
{
    public class Exchange
    {
        private IEnumerable<Ordem> _ordensDeCompra = new List<Ordem>();
        private IEnumerable<Ordem> _ordensDeVenda = new List<Ordem>();

        public Enumeradores.Enumeradores.Exchanges Nome { get; }
        public double PrecoOfertaAtual { get; }
        public double PrecoUltimaOfertaEfetivada { get; }
        public double PrecoOfertaMaisAltaDoDia { get; }
        public double PrecoOfertaMaisBaixaDoDia { get; }
        public double PrecoVendaEstimadoPelaExchange { get; }
        public double Taxas { get; }

        public IEnumerable<Ordem> OrdensDeCompra
        {
            get
            {
                return _ordensDeCompra;
            }
        }

        public IEnumerable<Ordem> OrdensDeVenda
        {
            get
            {
                return _ordensDeVenda;
            }
        }

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

        public void AdicionarOrdens(IEnumerable<Ordem> ordens)
        {
            _ordensDeCompra = _ordensDeCompra
                .Concat(ordens.Where(x => x.TipoDeOrdem == Enumeradores.Enumeradores.TipoDeOrdem.Compra));

            _ordensDeVenda = _ordensDeVenda
                .Concat(ordens.Where(x => x.TipoDeOrdem == Enumeradores.Enumeradores.TipoDeOrdem.Venda));
        }

    }
}
