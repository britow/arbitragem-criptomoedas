
namespace Arbitragem.Dominio.Arbitragens
{
    public class ResultadoArbitragem
    {
        public Enumeradores.Enumeradores.Exchanges ComprarDe { get; }
        public double PrecoDeCompra { get; }
        public double PrecoDeVenda { get; }
        public Enumeradores.Enumeradores.Exchanges VendarPara { get; }

        public double Porcentagem { get; }


        public ResultadoArbitragem(Enumeradores.Enumeradores.Exchanges comprarDe,
            Enumeradores.Enumeradores.Exchanges venderPara, double porcentagem, 
            double precoDeCompra, double precoDeVenda)
        {
            ComprarDe = comprarDe;
            VendarPara = venderPara;
            Porcentagem = porcentagem;
            PrecoDeCompra = precoDeCompra;
            PrecoDeVenda = precoDeVenda;
        }


    }
}
