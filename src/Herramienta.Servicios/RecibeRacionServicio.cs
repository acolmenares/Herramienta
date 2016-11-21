using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Objetivos;
using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.Servicios
{
	public class RecibeRacionServicio:Service
	{
		public  IRecibeRacionGestor Gestor { get; set; }

		public QueryResponse<RecibeRacion> Get(RecibeRacionConsultar peticion)
		{
			return Gestor.Consultar(peticion);
		}

		public ResponseCreate Post(RecibeRacionCrear peticion)
		{
			return Gestor.Crear(peticion);
		}

		public ResponseDelete Post(RecibeRacionBorrar peticion)
		{
			return Gestor.Borrar(peticion);
		}
	}
}

