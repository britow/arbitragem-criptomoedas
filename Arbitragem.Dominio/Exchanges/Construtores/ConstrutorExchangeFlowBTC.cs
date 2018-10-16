using Arbitragem.Dominio.Exceptions;
using Arbitragem.Dominio.Exchanges.ServicosHttp;
using System.Threading.Tasks;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class ConstrutorExchangeFlowBTC : ConstrutorExchange
    {
        private readonly FlowBTCServicoHttp _flowBtcServico;

        public ConstrutorExchangeFlowBTC(FlowBTCServicoHttp flowBtcServico)
        {
            _flowBtcServico = flowBtcServico;
        }

        public override async Task Construir()
        {
            exchange = await _flowBtcServico.ObterInformacoesDaExchange();

            if (exchange == null)
                throw new ExcecaoArbitragem($"Exchange {Enumeradores.Enumeradores.Exchanges.FlowBTC} não retornou dados.");

            var ordens = await _flowBtcServico.ObterOrdensDaExchange();

            exchange.AdicionarOrdens(ordens);
        }
    }
}
