namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class FabricaDeContrutoresDeExchanges
    {
        private readonly ConstrutorExchangeBitcoinTrade _construtorExchangeBitcoinTrade;
        private readonly ConstrutorExchangeMercadoBitcoin _construtorExchangeMercadoBitcoin;

        public FabricaDeContrutoresDeExchanges(ConstrutorExchangeBitcoinTrade construtorExchangeBitcoinTrade,
            ConstrutorExchangeMercadoBitcoin construtorExchangeMercadoBitcoin)
        {
            _construtorExchangeBitcoinTrade = construtorExchangeBitcoinTrade;
            _construtorExchangeMercadoBitcoin = construtorExchangeMercadoBitcoin;
        }

        public ConstrutorExchange Criar(Enumeradores.Enumeradores.Exchanges exchanges)
        {
            switch (exchanges)
            {
                case Enumeradores.Enumeradores.Exchanges.BitcoinTrade:
                    return _construtorExchangeBitcoinTrade;
                case Enumeradores.Enumeradores.Exchanges.MercadoBitcoin:
                    return _construtorExchangeMercadoBitcoin;
                default:
                    return _construtorExchangeBitcoinTrade;
            }
        }
    }
}
