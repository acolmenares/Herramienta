using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{
	public interface INecesidadBasicaGestor<T> : INecesidadBasicaGestorConsultas<T>
	where T : INecesidadBasica
	{
		ResponseCreate Crear(ITengoFechaRadicacionDesdeHasta rangoFechas);
		ResponseDelete Borrar(ITengoFechaRadicacionDesdeHasta rangoFechas);
	}

	public interface INecesidadBasicaGestorConsultas<T> where T : INecesidadBasica
	{
		QueryResponse<T> Consultar(ITengoFechaRadicacionDesdeHasta rangoFechas);
		//TODO bool ExisteDatosEntradaObjetivo<T> ...
	}

}
