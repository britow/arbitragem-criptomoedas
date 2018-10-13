using Arbitragem.Dominio.Exchanges;
using System.Threading.Tasks;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public interface IConstrutorGeralDeExchanges
    {
        Exchange[] Exchanges { get; }
        Task ConstruirTodas();
    }
}
