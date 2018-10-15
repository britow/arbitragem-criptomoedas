using System.Threading.Tasks;
using Arbitragem.Dominio.Exceptions;
using Arbitragem.Dominio.Exchanges.ServicosHttp;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class ConstrutorExchangeBitCambio : ConstrutorExchange
    {
        private readonly BitCambioServicoHttp _bitcoinCambioServicoHttp;

        public ConstrutorExchangeBitCambio(BitCambioServicoHttp bitcoinCambioServicoHttp)
        {
            _bitcoinCambioServicoHttp = bitcoinCambioServicoHttp;
        }

        public override async Task Construir()
        {
            exchange = await _bitcoinCambioServicoHttp.ObterInformacoesDaExchange();

            if (exchange == null)
                throw new ExcecaoArbitragem($"Exchange {Enumeradores.Enumeradores.Exchanges.BitCambio} não retornou dados.");

            var ordens = await _bitcoinCambioServicoHttp.ObterOrdensDaExchange();

            exchange.AdicionarOrdens(ordens);
        }
    }
}
