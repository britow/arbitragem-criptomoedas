
using System.Collections.Generic;
using System.Linq;
using Arbitragem.Dominio.Exchanges;

namespace Arbitragem.Dominio.Arbitragens
{
    public class ResultadoArbitragem
    {

        public double QuantidadeParaSerComprada { get;}
        public Enumeradores.Enumeradores.Exchanges ComprarDe { get; }
        public Enumeradores.Enumeradores.Exchanges VendarPara { get; }
        public IEnumerable<Ordem> OrdensParaCompra { get; }
        public IEnumerable<Ordem> OrdensParaVenda { get; }
        public double Porcentagem { get; }


        public ResultadoArbitragem(Enumeradores.Enumeradores.Exchanges comprarDe,
            Enumeradores.Enumeradores.Exchanges venderPara,
            double quantidadeParaSerComprada,
            IEnumerable<Ordem> ordensParaVenda, 
            IEnumerable<Ordem> ordensParaCompra)
        {
            ComprarDe = comprarDe;
            VendarPara = venderPara;
            QuantidadeParaSerComprada = quantidadeParaSerComprada;
            OrdensParaCompra = ordensParaCompra;
            OrdensParaVenda = ordensParaVenda;

            Porcentagem = CalcularPorcentagemLucro();
        }

        private double CalcularPorcentagemLucro()
        {
            if (!OrdensParaCompra.Any() || !OrdensParaVenda.Any()) return 0d;

            var totalPrecoDeCompraPorQuantidade = OrdensParaCompra
                .Sum(x => (x.Preco * x.Quantidade));

            var totalPrecoDeVendaPorQuantidade = OrdensParaVenda
                .Sum(x => (x.Preco * x.Quantidade));

            //new cost - previous cost / previous cost
            var porcentagemDeGanho = ((totalPrecoDeVendaPorQuantidade - totalPrecoDeCompraPorQuantidade) / totalPrecoDeCompraPorQuantidade) * 100;
            

            return porcentagemDeGanho;
        }
    }
}
