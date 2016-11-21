using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Metadata;
using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.Servicios
{
	public class RangoServicio:Service
	{
		public IRangoGestor RangoGestor { get; set; }

		public QueryResponse<Rango> Get(RangoConsultar peticion)
		{
			return RangoGestor.Consultar(peticion);
		}

		public ResponseCreate Post(RangoCrear peticion)
		{
			return RangoGestor.Crear(peticion);
		}

		public ResponseDelete Post(RangoBorrar peticion)
		{
			return RangoGestor.Borrar(peticion);
		}


	}
}

