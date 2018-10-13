namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class FabricaDeContrutoresDeExchanges
    {
        private readonly ConstrutorExchangeBitcoinTrade _construtorExchangeBitcoinTrade;

        public FabricaDeContrutoresDeExchanges(ConstrutorExchangeBitcoinTrade construtorExchangeBitcoinTrade)
        {
            _construtorExchangeBitcoinTrade = construtorExchangeBitcoinTrade;
        }

        public ConstrutorExchange Criar(Enumeradores.Enumeradores.Exchanges exchanges)
        {
            switch (exchanges)
            {
                case Enumeradores.Enumeradores.Exchanges.BitcoinTrade:
                    return _construtorExchangeBitcoinTrade;
                default:
                    return _construtorExchangeBitcoinTrade;
            }
        }
    }
}
