using System.Collections.Generic;

namespace Arbitragem.Dominio.Arbitragens.Acoes
{
    public abstract class Acao
    {
        public abstract void Executar(IEnumerable<ResultadoArbitragem> resultados);
    }
}
