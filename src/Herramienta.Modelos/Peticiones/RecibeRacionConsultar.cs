using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Objetivos;

namespace Herramienta.Modelos.Peticiones
{
	public class RecibeRacionConsultar:PeticionConsultarPorFecha<RecibeRacion>, ITengoFechaRadicacionDesdeHasta
	{	
		public string Municipio { get; set; }
		
	}
}
