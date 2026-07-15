using GGH.Application.Features.Resumenes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerTopCategoriasIngreso
{
    public class ObtenerTopCategoriasIngresoQuery : IRequest<IEnumerable<TopCategoriaDto>>
    {
        public DateOnly Desde { get; set; }
        public DateOnly Hasta { get; set; }
        public int Limite { get; set; } = 5;
    }
}
