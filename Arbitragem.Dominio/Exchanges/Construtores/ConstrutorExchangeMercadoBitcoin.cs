using Arbitragem.Dominio.Exceptions;
using Arbitragem.Dominio.Exchanges.ServicosHttp;
using System.Threading.Tasks;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class ConstrutorExchangeMercadoBitcoin : ConstrutorExchange
    {
        private readonly MercadoBitcoinServicoHttp _mercadoBitcoinServicoHttp;

        public ConstrutorExchangeMercadoBitcoin(MercadoBitcoinServicoHttp mercadoBitcoinServicoHttp)
        {
            _mercadoBitcoinServicoHttp = mercadoBitcoinServicoHttp;
        }

        public override async Task Construir()
        {
            exchange = await _mercadoBitcoinServicoHttp.ObterInformacoesDaExchange();

            if (exchange == null)
                throw new ExcecaoArbitragem($"Exchange {Enumeradores.Enumeradores.Exchanges.BitcoinTrade} não retornou dados.");

            var ordens = await _mercadoBitcoinServicoHttp.ObterOrdensDaExchange();

            exchange.AdicionarOrdens(ordens);
        }
    }
}
