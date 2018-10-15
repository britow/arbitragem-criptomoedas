using Arbitragem.Dominio.Exceptions;
using Arbitragem.Dominio.Exchanges.ServicosHttp;
using System.Threading.Tasks;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class ConstrutorExchangeBraziliex : ConstrutorExchange
    {
        private readonly BraziliexServicoHttp _braziliexServicoHttp;

        public ConstrutorExchangeBraziliex(BraziliexServicoHttp braziliexServicoHttp)
        {
            _braziliexServicoHttp = braziliexServicoHttp;
        }

        public override async Task Construir()
        {
            exchange = await _braziliexServicoHttp.ObterInformacoesDaExchange();

            if (exchange == null)
                throw new ExcecaoArbitragem($"Exchange {Enumeradores.Enumeradores.Exchanges.Braziliex} não retornou dados.");

            var ordens = await _braziliexServicoHttp.ObterOrdensDaExchange();

            exchange.AdicionarOrdens(ordens);
        }

    }
}
