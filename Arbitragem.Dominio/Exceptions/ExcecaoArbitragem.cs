using System;

namespace Arbitragem.Dominio.Exceptions
{
    public class ExcecaoArbitragem : Exception
    {
        public ExcecaoArbitragem(string mensagem)
            :base(mensagem)
        {

        }
    }
}
