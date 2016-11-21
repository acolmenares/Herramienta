
namespace Herramienta.Modelos.Interfaces
{
	
	public interface IRangoGestor: IRangoGestorConsultas
	{
		ResponseCreate Crear(ITengoFechaRadicacionDesdeHasta peticion);
		ResponseDelete Borrar(ITengoFechaRadicacionDesdeHasta peticion);

		// TODO CrearDatosEntradaObjetivo<T> ....

	}

}
