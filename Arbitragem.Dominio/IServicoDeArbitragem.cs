using System.Threading.Tasks;

namespace Arbitragem.Dominio
{
    public interface IServicoDeArbitragem
    {
        Task Iniciar(double quantidadeDeBitcoinsParaNegociar);
    }
}
