using System.Collections.Generic;
using System.Linq;
using Arbitragem.Dominio.Arbitragens;

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

        public IEnumerable<Ordem> OrdensDeCompra => _ordensDeCompra;

        public IEnumerable<Ordem> OrdensDeVenda => _ordensDeVenda;

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
            var arrayDeOrdens = ordens as Ordem[] ?? ordens.ToArray();

            _ordensDeCompra = _ordensDeCompra
                .Concat(arrayDeOrdens.Where(x => x.TipoDeOrdem == Enumeradores.Enumeradores.TipoDeOrdem.Compra));

            _ordensDeVenda = _ordensDeVenda
                .Concat(arrayDeOrdens.Where(x => x.TipoDeOrdem == Enumeradores.Enumeradores.TipoDeOrdem.Venda));
        }

        public ResultadoArbitragem ObterDadosDeComparacaoDeVendaCompraEntreExchanges(Exchange exchange, double quantidadeDeBitcoinsParaNegociar)
        {
            var porcentagemDeGanho = ((exchange.PrecoVendaEstimadoPelaExchange - PrecoVendaEstimadoPelaExchange) / PrecoVendaEstimadoPelaExchange) * 100;

            if (porcentagemDeGanho <= 0) return null;

            var somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade = 0d;

            var ordensParaComprarQueAtendemPreco = OrdensDeVenda
                .Where(x => x.Preco < exchange.PrecoVendaEstimadoPelaExchange);

            var ordensParaComprarQueAtendemPrecoQuantidade = new List<Ordem>();

            foreach (var ordem in ordensParaComprarQueAtendemPreco)
            {
                var resultadoDaSomatoria = somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade + ordem.Quantidade;

                if (resultadoDaSomatoria > quantidadeDeBitcoinsParaNegociar)
                {
                    var diferencaParaAtingirQuantidadeDeBitcoinsParaNegociar = quantidadeDeBitcoinsParaNegociar - somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade;

                    somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade = quantidadeDeBitcoinsParaNegociar;

                    ordensParaComprarQueAtendemPrecoQuantidade.Add(new Ordem(ordem.CodigoDaOrdem, ordem.Preco,
                        diferencaParaAtingirQuantidadeDeBitcoinsParaNegociar, ordem.TipoDeOrdem));

                    break;
                }

                somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade += ordem.Quantidade;

                ordensParaComprarQueAtendemPrecoQuantidade.Add(ordem);
            }


            var somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade = 0d;

            var ordensParaVenderQueAtendemPreco = exchange.OrdensDeCompra
                .Where(x => x.Preco > PrecoVendaEstimadoPelaExchange);

            var ordensParaVenderQueAtendemPrecoQuantidade = new List<Ordem>();

            foreach (var ordem in ordensParaVenderQueAtendemPreco)
            {
                var resultadoDaSomatoria = somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade + ordem.Quantidade;

                if (resultadoDaSomatoria > somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade)
                {
                    var diferencaParaAtingirQuantidadeDeBitcoinsParaNegociar = somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade - somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade;

                    ordensParaVenderQueAtendemPrecoQuantidade.Add(new Ordem(ordem.CodigoDaOrdem, ordem.Preco,
                        diferencaParaAtingirQuantidadeDeBitcoinsParaNegociar, ordem.TipoDeOrdem));

                    somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade =
                        somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade;

                    break;
                }

                somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade += ordem.Quantidade;

                ordensParaVenderQueAtendemPrecoQuantidade.Add(ordem);
            }



            if (somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade > 0 &&
                somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade > 0 &&
                somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade <
                somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade)
            {
                var ordensOrdenadasPelasMaisCaras = ordensParaComprarQueAtendemPrecoQuantidade
                    .OrderByDescending(x => x.Preco);

                var subtracaoDeOrdensParaComprarQueAtendemPrecoQuantidade =
                    somatorioDeOrdensParaComprarQueAtendemPrecoQuantidade;

                foreach (var ordem in ordensOrdenadasPelasMaisCaras)
                {
                    subtracaoDeOrdensParaComprarQueAtendemPrecoQuantidade -=  ordem.Quantidade;

                    if (subtracaoDeOrdensParaComprarQueAtendemPrecoQuantidade < somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade)
                    {
                        var quantidadeParaIgualarVendaCompra = somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade - subtracaoDeOrdensParaComprarQueAtendemPrecoQuantidade;

                        ordensParaComprarQueAtendemPrecoQuantidade.Remove(ordem);

                        ordensParaComprarQueAtendemPrecoQuantidade.Add(new Ordem(ordem.CodigoDaOrdem, ordem.Preco,
                            quantidadeParaIgualarVendaCompra, ordem.TipoDeOrdem));

                        subtracaoDeOrdensParaComprarQueAtendemPrecoQuantidade =
                            somatorioDeOrdensParaVenderQueAtendemPrecoQuantidade;

                        break;
                    }

                    ordensParaComprarQueAtendemPrecoQuantidade.Remove(ordem);
                }

            }

            return new ResultadoArbitragem(Nome, exchange.Nome, quantidadeDeBitcoinsParaNegociar, ordensParaVenderQueAtendemPrecoQuantidade,
                ordensParaComprarQueAtendemPrecoQuantidade);
        }

    }
}
