namespace Arbitragem.Dominio.Exchanges
{
    public class Ordem
    {
        public double Preco { get; }
        public double Quantidade { get; }
        public string CodigoDaOrdem { get; }
        public Enumeradores.Enumeradores.TipoDeOrdem TipoDeOrdem { get; }

        public Ordem(string codigoDaOrdem, double preco, double quantidade,
            Enumeradores.Enumeradores.TipoDeOrdem tipoDeOrdem)
        {
            CodigoDaOrdem = codigoDaOrdem;
            Preco = preco;
            Quantidade = quantidade;
            TipoDeOrdem = tipoDeOrdem;
        }
    }
}
