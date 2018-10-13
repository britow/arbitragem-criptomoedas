using System.Linq;
using System.Threading.Tasks;
using Arbitragem.Dominio.Exceptions;
using Arbitragem.Dominio.Exchanges.ServicosHttp;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class ConstrutorExchangeBitcoinTrade : ConstrutorExchange
    {
        private readonly BitcoinTradeServicoHttp _bitcoinTradeServicoHttp;

        public ConstrutorExchangeBitcoinTrade(BitcoinTradeServicoHttp bitcoinTradeServicoHttp)
        {
            _bitcoinTradeServicoHttp = bitcoinTradeServicoHttp;
        }

        public override async Task Construir()
        {
            exchange = await _bitcoinTradeServicoHttp.ObterInformacoesDaExchange();

            if (exchange == null)
                throw new ExcecaoArbitragem($"Exchange {Enumeradores.Enumeradores.Exchanges.BitcoinTrade} não retornou dados.");

            var ordens = await _bitcoinTradeServicoHttp.ObterOrdensDaExchange();

            exchange.AdicionarOrdens(ordens);
        }

    }
}
