using System.Threading.Tasks;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public abstract class ConstrutorExchange
    {
        protected Exchange exchange;

        public Exchange Exchange => exchange;

        public abstract Task Construir();
    }
}
