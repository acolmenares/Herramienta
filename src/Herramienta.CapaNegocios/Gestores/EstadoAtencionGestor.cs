using System;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;using Herramienta.Modelos.Objetivos;using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.CapaNegocios.Gestores
{
	public class EstadoAtencionGestor : NecesidadBasicaGestor<EstadoAtencion>,IEstadoAtencionGestor
	{
		readonly IFiltroListados<RecibeRacion, EstadoAtencion> filtro;
		public EstadoAtencionGestor(IHerramientaEnCache cacheClient,
									IRangoGestorConsultas rangoGestor,
									IRepositorioNecesidadBasica repositorioEstadoAtencion,
									IGeneradorNecesidadBasica<EstadoAtencion> generadorEstadoAtencion,		                            IFiltroListados<RecibeRacion, EstadoAtencion> filtro
								   )			: base(cacheClient, rangoGestor, repositorioEstadoAtencion, generadorEstadoAtencion)
		{
			this.filtro = filtro;		}


		public QueryResponse<EstadoAtencion> Consultar(EstadoAtencionConsultar peticion)
		{			var response =base.Consultar(peticion);			FiltrarResponse(response, peticion);
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
