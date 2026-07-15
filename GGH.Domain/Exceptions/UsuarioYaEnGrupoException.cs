using System;
using System.Collections.Generic;
using System.Text;

namespace GGH.Domain.Exceptions
{
    public class UsuarioYaEnGrupoException : Exception
    {
        public UsuarioYaEnGrupoException()
            : base("El usuario ya pertenece a un grupo familiar.") { }
    }
}
