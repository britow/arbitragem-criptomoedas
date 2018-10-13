using System.Collections.Generic;
using System.Threading.Tasks;
using Arbitragem.Dominio.Exchanges.Construtores;

namespace Arbitragem.Dominio
{
    public class ServicoDeArbitragem : IServicoDeArbitragem
    {
        private readonly IConstrutorGeralDeExchanges _construtorGeralDeExchanges;
        public ServicoDeArbitragem(IConstrutorGeralDeExchanges construtorGeralDeExchanges)
        {
            _construtorGeralDeExchanges = construtorGeralDeExchanges;
        }

        public async Task Iniciar()
        {
            await _construtorGeralDeExchanges.ConstruirTodas();

            var possiveisArbitragens = ObterListaDePossiveisArbitragens();
        }

        private static IEnumerable<Arbitragens.Arbitragem> ObterListaDePossiveisArbitragens()
        {
           return new List<Arbitragens.Arbitragem>();
        }
    }

    
}
