﻿using System;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;
using ServiceStack;

namespace Herramienta.CapaNegocios.Gestores
{
	public class EstadoAtencionGestor : NecesidadBasicaGestor<EstadoAtencion>,IEstadoAtencionGestor
	{
		readonly IFiltroListados<RecibeRacion, EstadoAtencion> filtro;

									IRangoGestorConsultas rangoGestor,
									IRepositorioNecesidadBasica repositorioEstadoAtencion,
									IGeneradorNecesidadBasica<EstadoAtencion> generadorEstadoAtencion,
								   )
		{
			this.filtro = filtro;


		public QueryResponse<EstadoAtencion> Consultar(EstadoAtencionConsultar peticion)
		{
			return response;
		}


		public ResponseDelete Borrar(EstadoAtencionBorrar peticion)
		{
			return base.Borrar(peticion);
		}

		public ResponseCreate Crear(EstadoAtencionCrear peticion)
		{
			return base.Crear(peticion);
		}

		void FiltrarResponse(QueryResponse<EstadoAtencion> response, EstadoAtencionConsultar peticion)
		{
			if (peticion.Indicador.EstaVacia()) return;
			var grupo = new RecibeRacion();
			grupo.PopulateWith(peticion);
			response.Results= filtro.FiltrarPor(peticion.Indicador, grupo, response.Results);
			response.Total = response.Results.Count;
		}
	}
}