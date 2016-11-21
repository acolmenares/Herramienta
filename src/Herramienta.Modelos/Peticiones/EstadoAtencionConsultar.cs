using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Objetivos;

namespace Herramienta.Modelos.Peticiones
{
	public class EstadoAtencionConsultar : PeticionConsultarPorFecha<EstadoAtencion>
	{
		public string Periodo { get; set; }
		public string Regional { get; set; }
		public string Municipio { get; set; }
		public string AnioMes { get; set; }
		public string Indicador { get; set; }
	}
}

