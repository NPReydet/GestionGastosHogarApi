using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Resumenes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerTopCategoriasIngreso
{
    public class ObtenerTopCategoriasIngresoHandler : IRequestHandler<ObtenerTopCategoriasIngresoQuery, IEnumerable<TopCategoriaDto>>
    {
        private readonly IRepositorioResumenes _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ObtenerTopCategoriasIngresoHandler(IRepositorioResumenes repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<TopCategoriaDto>> Handle(ObtenerTopCategoriasIngresoQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ObtenerTopCategoriasIngresoAsync(_usuarioActual.UsuarioId, request.Desde, request.Hasta, request.Limite);
        }
    }
}
