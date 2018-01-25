using System;

namespace CoruscantMarketplace.Crosscutting.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string mensagem)
            : base(mensagem)
        {

        }
    }
}
