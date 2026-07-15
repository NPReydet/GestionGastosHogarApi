using System;
using System.Collections.Generic;
using System.Text;

namespace GGH.Domain.Exceptions
{
    public class GastoNoEncontradoException : Exception
    {
        public GastoNoEncontradoException()
            : base("No existe el gasto indicado.") { }
    }
}
