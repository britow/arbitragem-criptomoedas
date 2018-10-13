using System.Threading.Tasks;
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
        }

    }
}
