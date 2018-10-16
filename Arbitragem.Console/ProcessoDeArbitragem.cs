using Arbitragem.Dominio;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Arbitragem.Console
{
    public class ProcessoDeArbitragem : IHostedService
    {
        internal readonly IServicoDeArbitragem ServicoDeArbitragem;
        internal readonly TimeSpan IntervaloDeExecucaoEmMinutos;
        internal readonly CultureInfo CulturaInfo;

        public ProcessoDeArbitragem(IServicoDeArbitragem servicoDeArbitragem, IConfiguration configuration)
        {
            ServicoDeArbitragem = servicoDeArbitragem;

            int.TryParse(configuration["Configuracoes:IntervaloDeExecucaoEmMinutos"],
                out var intervaloEmMinutos);

            IntervaloDeExecucaoEmMinutos = new TimeSpan(0, 0, intervaloEmMinutos, 0);

            CulturaInfo = new CultureInfo(configuration["Configuracoes:CulturaInfo"]);

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CulturaInfo;
                Thread.CurrentThread.CurrentUICulture = CulturaInfo;

                var tarefaDeArbitragem = await Task.Factory.StartNew(async () =>
                    {
                        await ServicoDeArbitragem.Iniciar();
                    },
                    cancellationToken);

                while (true)
                {
                    if (!tarefaDeArbitragem.IsCompleted) continue;

                    tarefaDeArbitragem.Dispose();

                    await Task.Delay(IntervaloDeExecucaoEmMinutos, cancellationToken);

                    tarefaDeArbitragem = await Task.Factory.StartNew(async () =>
                    {
                        await ServicoDeArbitragem.Iniciar();
                    }, cancellationToken);
                }

            }
            catch (Exception excecao)
            {
                System.Console.WriteLine($"Ocorreu uma excecao: {excecao}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
