namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public class FabricaDeContrutoresDeExchanges
    {
        private readonly ConstrutorExchangeBitcoinTrade _construtorExchangeBitcoinTrade;
        private readonly ConstrutorExchangeMercadoBitcoin _construtorExchangeMercadoBitcoin;
        private readonly ConstrutorExchangeBitCambio _construtorExchangeBitCambio;

        public FabricaDeContrutoresDeExchanges(ConstrutorExchangeBitcoinTrade construtorExchangeBitcoinTrade,
            ConstrutorExchangeMercadoBitcoin construtorExchangeMercadoBitcoin, ConstrutorExchangeBitCambio construtorExchangeBitCambio)
        {
            _construtorExchangeBitcoinTrade = construtorExchangeBitcoinTrade;
            _construtorExchangeMercadoBitcoin = construtorExchangeMercadoBitcoin;
            _construtorExchangeBitCambio = construtorExchangeBitCambio;
        }

        public ConstrutorExchange Criar(Enumeradores.Enumeradores.Exchanges exchanges)
        {
            switch (exchanges)
            {
                case Enumeradores.Enumeradores.Exchanges.BitcoinTrade:
                    return _construtorExchangeBitcoinTrade;
                case Enumeradores.Enumeradores.Exchanges.MercadoBitcoin:
                    return _construtorExchangeMercadoBitcoin;
                case Enumeradores.Enumeradores.Exchanges.BitCambio:
                    return _construtorExchangeBitCambio;
                default:
                    return _construtorExchangeBitcoinTrade;
            }
        }
    }
}
