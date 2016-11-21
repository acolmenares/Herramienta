using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{
	public interface IIndicadorNecesidadBasicaGestor<T> : IIndicadorNecesidadBasicaGestorConsultas<T>
		where T : IIndicadorNecesidadBasica
	{
		ResponseCreate Crear(ITengoFechaRadicacionDesdeHasta rangoFechas);
		ResponseDelete Borrar(ITengoFechaRadicacionDesdeHasta rangoFechas);
	}


	public interface IIndicadorNecesidadBasicaGestorConsultas<T> where T : IIndicadorNecesidadBasica
	{
		QueryResponse<T> Consultar(ITengoFechaRadicacionDesdeHasta peticion);
	}
}
