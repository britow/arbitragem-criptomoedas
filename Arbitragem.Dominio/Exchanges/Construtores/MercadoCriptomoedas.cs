using System.Threading.Tasks;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class MercadoCriptomoedas
    {
        public async Task Montar(ConstrutorExchange construtorExchange)
        {
            await construtorExchange.Construir();
        }
    }
}
