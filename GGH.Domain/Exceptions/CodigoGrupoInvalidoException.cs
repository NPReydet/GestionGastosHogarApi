using System;
using System.Collections.Generic;
using System.Text;

namespace GGH.Domain.Exceptions
{
    public class CodigoGrupoInvalidoException : Exception
    {
        public CodigoGrupoInvalidoException(string mensaje) : base(mensaje) { }
    }
}
