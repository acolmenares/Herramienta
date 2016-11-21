using Herramienta.Modelos.Objetivos;
using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{
	public interface IRecibeRacionGestor:IIndicadorNecesidadBasicaGestorConsultas<RecibeRacion>
	{
		QueryResponse<RecibeRacion> Consultar(RecibeRacionConsultar peticion);
		ResponseCreate Crear(RecibeRacionCrear peticion);
		ResponseDelete Borrar(RecibeRacionBorrar peticion);
	}
}
