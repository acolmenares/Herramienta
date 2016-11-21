using Herramienta.Modelos.Objetivos;
using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{
	public interface IEstadoAtencionGestor:INecesidadBasicaGestorConsultas<EstadoAtencion>
	{
		QueryResponse<EstadoAtencion> Consultar(EstadoAtencionConsultar peticion);

		ResponseCreate Crear(EstadoAtencionCrear peticion);

		ResponseDelete Borrar(EstadoAtencionBorrar peticion);
	}
}
