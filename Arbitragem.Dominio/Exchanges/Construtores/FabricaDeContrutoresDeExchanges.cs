namespace Arbitragem.Dominio.Exchanges.Construtores
{
    public sealed class FabricaDeContrutoresDeExchanges
    {
        private readonly ConstrutorExchangeBitcoinTrade _construtorExchangeBitcoinTrade;
        private readonly ConstrutorExchangeMercadoBitcoin _construtorExchangeMercadoBitcoin;
        private readonly ConstrutorExchangeBitCambio _construtorExchangeBitCambio;
        private readonly ConstrutorExchangeBraziliex _construtorExchangeBraziliex;
        private readonly ConstrutorExchangeFlowBTC _construtorExchangeFlowBtc;

        public FabricaDeContrutoresDeExchanges(ConstrutorExchangeBitcoinTrade construtorExchangeBitcoinTrade,
            ConstrutorExchangeMercadoBitcoin construtorExchangeMercadoBitcoin,
            ConstrutorExchangeBitCambio construtorExchangeBitCambio,
            ConstrutorExchangeBraziliex construtorExchangeBraziliex,
            ConstrutorExchangeFlowBTC construtorExchangeFlowBtc)
        {
            _construtorExchangeBitcoinTrade = construtorExchangeBitcoinTrade;
            _construtorExchangeMercadoBitcoin = construtorExchangeMercadoBitcoin;
            _construtorExchangeBitCambio = construtorExchangeBitCambio;
            _construtorExchangeBraziliex = construtorExchangeBraziliex;
            _construtorExchangeFlowBtc = construtorExchangeFlowBtc;
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
                case Enumeradores.Enumeradores.Exchanges.Braziliex:
                    return _construtorExchangeBraziliex;
                case Enumeradores.Enumeradores.Exchanges.FlowBTC:
                    return _construtorExchangeFlowBtc;
                default:
                    return _construtorExchangeBitcoinTrade;
            }
        }
    }
}
