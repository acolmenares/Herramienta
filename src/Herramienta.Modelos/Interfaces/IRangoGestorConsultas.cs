using Herramienta.Modelos.Metadata;
using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{

	public interface IRangoGestorConsultas
	{
		QueryResponse<Rango> Consultar(RangoConsultar peticion);
		TablasRango ConsultarTablasRango(ITengoFechaRadicacionDesdeHasta peticion);
	}

}
