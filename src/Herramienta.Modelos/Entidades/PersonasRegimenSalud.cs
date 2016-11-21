using System;
using Herramienta.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace Herramienta.Modelos.Entidades
{
	[Alias("Personas_Regimen_Salud")]
	public class PersonasRegimenSalud: IEntidad, IConIdPersona
	{
		public int Id { get; set; }
		public int? Id_Persona { get; set; }
		public int? Id_Tipo_Entrega { get; set; }
		public int? Id_Regimen_Salud { get; set; }
		public int? Id_Eps { get; set; }
		public string Municipio { get; set; }
		public DateTime? Fecha { get; set; }
		public int? Id_Necesita_Traslado { get; set; }
		public int? Id_Motivo_No_Necesita_Traslado { get; set; }
		public int? Id_Realizo_Traslado { get; set; }
		public int? Id_Motivo_No_Realizo_Traslado { get; set; }
		public string Observaciones { get; set; }
		public int? Id_Cerrar { get; set; }
		public int? Id_Declaracion_Seguimiento { get; set; }
		public int? Id_Municipio { get; set; }
	}
}