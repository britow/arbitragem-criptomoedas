using System.Threading.Tasks;
using Arbitragem.Dominio.Arbitragens.Acoes;
using Arbitragem.Dominio.Exchanges.Construtores;

namespace Arbitragem.Dominio
{
    public class ServicoDeArbitragem : IServicoDeArbitragem
    {
        private readonly IConstrutorGeralDeExchanges _construtorGeralDeExchanges;

        private readonly Acao _acao;

        public ServicoDeArbitragem(IConstrutorGeralDeExchanges construtorGeralDeExchanges, Acao acao)
        {
            _construtorGeralDeExchanges = construtorGeralDeExchanges;
            _acao = acao;
        }

        public async Task Iniciar(double quantidadeDeBitcoinsParaNegociar)
        {
            await _construtorGeralDeExchanges.ConstruirTodas();

            var arbitragem = new Arbitragens.Arbitragem(_construtorGeralDeExchanges.Exchanges, quantidadeDeBitcoinsParaNegociar);

            var resultados = arbitragem.ObterResultadoDaArbitragem();

            _acao.Executar(resultados);
        }


    }


}
