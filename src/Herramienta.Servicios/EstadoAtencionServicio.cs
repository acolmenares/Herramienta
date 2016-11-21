using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Objetivos;
using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.Servicios
{
	public class EstadoAtencionServicio:Service
	{
		public IEstadoAtencionGestor Gestor { get; set; }

		public QueryResponse<EstadoAtencion> Get(EstadoAtencionConsultar peticion)
		{
			return Gestor.Consultar(peticion);
		}


		public ResponseCreate Post(EstadoAtencionCrear peticion)
		{
			return Gestor.Crear(peticion);
		}

		public ResponseDelete Post(EstadoAtencionBorrar peticion)
		{
			return Gestor.Borrar(peticion);
		}

	}
}

