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
            System.Console.WriteLine($"-------------------- Resultados --------------------");

            var resultadoArbitragems = resultados as ResultadoArbitragem[] ?? resultados.ToArray();

            var resultadoFiltrado = resultadoArbitragems.Where(x => x.Porcentagem > 0)
                .OrderByDescending(x => x.Porcentagem);

            foreach (var resultado in resultadoFiltrado)
            {
                System.Console.WriteLine(
                    $"Caso compre Bitcoins da {resultado.ComprarDe} e venda para {resultado.VendarPara},{Environment.NewLine}voce ira ganhar uma porcentagem de {resultado.Porcentagem:0.00}%");
            }
        }
    }
}
