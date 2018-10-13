using System;
using System.Threading.Tasks;

namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class ConstrutorGeralDeExchanges : IConstrutorGeralDeExchanges
    {
        private readonly FabricaDeContrutoresDeExchanges _fabricaDeContrutoresDeExchanges;

        public ConstrutorGeralDeExchanges(FabricaDeContrutoresDeExchanges fabricaDeContrutoresDeExchanges)
        {
            _fabricaDeContrutoresDeExchanges = fabricaDeContrutoresDeExchanges;
        }

        public Exchange[] Exchanges { get; private set; } = new Exchange[Enum.GetNames(typeof(Enumeradores.Enumeradores.Exchanges)).Length];

        public async Task ConstruirTodas()
        {
            var mercadoCriptomoedas = new MercadoCriptomoedas();

            for (var i = 0; i < Enum.GetNames(typeof(Enumeradores.Enumeradores.Exchanges)).Length; i++)
            {
                var construtorExchange = _fabricaDeContrutoresDeExchanges
                    .Criar((Enumeradores.Enumeradores.Exchanges)i);

                await mercadoCriptomoedas.Montar(construtorExchange);

                Exchanges[i] = construtorExchange.Exchange;
            }
        }
    }
}
