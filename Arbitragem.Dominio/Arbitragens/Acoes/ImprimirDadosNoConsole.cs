using System.Collections.Generic;
using System.Linq;

namespace Arbitragem.Dominio.Arbitragens.Acoes
{
    public class ImprimirDadosNoConsole : Acao
    {
        public override void Executar(IEnumerable<ResultadoArbitragem> resultados)
        {
            var resultadoArbitragems = resultados as ResultadoArbitragem[] ?? resultados.ToArray();

            var resultadoFiltrado = resultadoArbitragems
                .Where(x => x.OrdensParaCompra.Any() && x.OrdensParaVenda.Any())
                .OrderByDescending(x => x.Porcentagem);

            System.Console.WriteLine("");
            System.Console.WriteLine("");

            foreach (var resultado in resultadoFiltrado)
            {
                System.Console.WriteLine($"-------------------- Comprar Exchange: {resultado.ComprarDe} - Vender Exchange: {resultado.VendarPara} --------------------");

                System.Console.WriteLine("");
                System.Console.WriteLine($"Quantidade Bitcoins para operar: {resultado.QuantidadeParaSerComprada}");
                System.Console.WriteLine($"Porcentagem de lucro: {resultado.Porcentagem:0.00}%");
                System.Console.WriteLine($"Quantidade de compra: {resultado.OrdensParaCompra.Sum(x => x.Quantidade)}");
                System.Console.WriteLine($"Quantidade de venda: {resultado.OrdensParaVenda.Sum(x => x.Quantidade)}");
                System.Console.WriteLine("");


                System.Console.WriteLine(
                    $"-------------------- Ordens de compra: {resultado.ComprarDe} --------------------");
                System.Console.WriteLine("");

                var ordensParaCompraOrdenado = resultado.OrdensParaCompra.OrderBy(x => x.Preco);

                foreach (var ordemCompra in ordensParaCompraOrdenado)
                {
                    System.Console.WriteLine($"Quantidade: {ordemCompra.Quantidade}");
                    System.Console.WriteLine($"Preco: {ordemCompra.Preco}");
                    System.Console.WriteLine("");
                }

                System.Console.WriteLine(
                    $"-------------------- Ordens de venda: {resultado.VendarPara} --------------------");
                System.Console.WriteLine("");

                var ordensParaVendaOrdenado = resultado.OrdensParaVenda.OrderBy(x => x.Preco);

                foreach (var ordemVenda in ordensParaVendaOrdenado)
                {
                    System.Console.WriteLine($"Quantidade: {ordemVenda.Quantidade}");
                    System.Console.WriteLine($"Preco: {ordemVenda.Preco}");
                    System.Console.WriteLine("");
                }

                System.Console.WriteLine("");
                System.Console.WriteLine("");
                System.Console.WriteLine("");

            }
        }

    }

}
