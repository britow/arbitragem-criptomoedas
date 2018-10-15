using System.Collections.Generic;
using System.Linq;
using Arbitragem.Dominio.Exceptions;
using Arbitragem.Dominio.Exchanges;

namespace Arbitragem.Dominio.Arbitragens
{
    public class Arbitragem
    {
        private IEnumerable<Exchange> _exchanges = new List<Exchange>();

        public IEnumerable<Exchange> Exchanges => _exchanges;
        public double QuantidadeDeBitcoinsParaNegociar { get; }

        public Arbitragem(IEnumerable<Exchange> exchanges, double quantidadeDeBitcoinsParaNegociar)
        {
            var arrayDeExchanges = exchanges as Exchange[] ?? exchanges?.ToArray();

            if (arrayDeExchanges == null || !arrayDeExchanges.Any())
                throw new ExcecaoArbitragem("Nenhuma Exchange foi informada para a arbitragem");

            if (quantidadeDeBitcoinsParaNegociar <= 0)
                throw new ExcecaoArbitragem("A quantia de Bitcoins nao foi informada para a arbitragem");

            _exchanges = arrayDeExchanges;

            QuantidadeDeBitcoinsParaNegociar = quantidadeDeBitcoinsParaNegociar;
        }

        public IEnumerable<ResultadoArbitragem> ObterResultadoDaArbitragem()
        {
            foreach (var exchange in _exchanges)
            {
                var todasExchangesExcetoAusadaParaComparacao = _exchanges.Except(new[] { exchange });

                foreach (var exchangeParaComparar in todasExchangesExcetoAusadaParaComparacao)
                {
                    var resultadoDeArbitragem = exchange
                        .ObterDadosDeComparacaoDeVendaCompraEntreExchanges(exchangeParaComparar);

                    yield return resultadoDeArbitragem;
                }
            }

        }
    }

    public class ResultadoArbitragem
    {
        public Enumeradores.Enumeradores.Exchanges ComprarDe { get; }
        public Enumeradores.Enumeradores.Exchanges VendarPara { get; }

        public double Porcentagem { get; }


        public ResultadoArbitragem(Enumeradores.Enumeradores.Exchanges comprarDe,
            Enumeradores.Enumeradores.Exchanges venderPara, double porcentagem)
        {
            ComprarDe = comprarDe;
            VendarPara = venderPara;
            Porcentagem = porcentagem;
        }


    }

}
