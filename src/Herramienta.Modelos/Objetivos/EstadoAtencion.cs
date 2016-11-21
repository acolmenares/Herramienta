using System;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Objetivos
{
	public class EstadoAtencion : NecesidadBasica, INecesidadBasica
	{
		public string Elegibilidad { get; set; }
		public DateTime? FechaElegibilidad { get; set; }
		public string Contactado { get; set; }
		public DateTime? FechaContactado { get; set; }
		public string Programado { get; set; }
		public DateTime? FechaProgramado { get; set; }
		public string Reprogramado { get; set; }
		public DateTime? FechaReprogramado { get; set; }
		public string Atendido { get; set; }
		public string MotivoNoAtencion { get; set; }
		public string TipoReprogramacion { get; set; }
		public int? MotivoNoAtencionId { get; set; }

		public string AsistioSegundaEntrega { get; set; }
		public DateTime? FechaSegundaEntrega { get; set; }

		public string AnioMesSegundaEntrega { get { return FechaSegundaEntrega.EnAnioMes(); } }
		public string AnioMesProgramado { get { return FechaProgramado.EnAnioMes(); } }
		public string AnioMesReprogramado { get { return FechaReprogramado.EnAnioMes(); } }
	}

}
