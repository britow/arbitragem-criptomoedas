using System;
using System.Collections.Generic;
using System.Linq;

namespace Arbitragem.Dominio.Arbitragens.Acoes
{
    public class ImprimirDadosNoConsole : Acao
    {
        public override void Executar(IEnumerable<ResultadoArbitragem> resultados)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("-------------------- Resultados --------------------");
            System.Console.WriteLine("");

            var resultadoArbitragems = resultados as ResultadoArbitragem[] ?? resultados.ToArray();

            var resultadoFiltrado = resultadoArbitragems.Where(x => x.Porcentagem > 0)
                .OrderByDescending(x => x.Porcentagem);

            foreach (var resultado in resultadoFiltrado)
            {
                System.Console.WriteLine(
                    $"Caso compre Bitcoins da {resultado.ComprarDe} preco atual {resultado.PrecoDeCompra:C} e venda{Environment.NewLine}para {resultado.VendarPara} preco atual {resultado.PrecoDeVenda:C}, voce ira ganhar uma porcentagem{Environment.NewLine}de {resultado.Porcentagem:0.00}%");

                System.Console.WriteLine("");
            }
        }

    }

}
