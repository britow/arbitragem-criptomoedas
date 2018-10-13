using Arbitragem.Dominio;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Arbitragem.Console
{
    public class ProcessoDeArbitragem : IHostedService
    {
        private readonly IServicoDeArbitragem _servicoDeArbitragem;

        public ProcessoDeArbitragem(IServicoDeArbitragem servicoDeArbitragem)
        {
            _servicoDeArbitragem = servicoDeArbitragem;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Iniciando...");

            await _servicoDeArbitragem.Iniciar();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Finalizando...");

            return Task.CompletedTask;
        }
    }
}
