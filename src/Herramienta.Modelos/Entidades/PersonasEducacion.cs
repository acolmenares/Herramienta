using System;
using Herramienta.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace Herramienta.Modelos.Entidades
{
	[Alias("Personas_Educacion")]
	public class PersonasEducacion: IEntidad, IConIdPersona
	{
		public int Id { get; set; }
		public int? Id_Persona { get; set; }
		public DateTime? Fecha { get; set; }
		public int? Id_Estudia_Actualmente { get; set; }
		public int? Id_Motivo_NoEstudia { get; set; }
		public int? Id_Certificado_Matricula { get; set; }
		public int? Id_Seguimiento { get; set; }
		public string Establecimiento { get; set; }
		public int? Id_Grado_Escolar { get; set; }
		public int? Verificado_Entidad { get; set; }
		public string Observaciones { get; set; }
		public int? Id_Tipo_Entrega { get; set; }
		public int? Id_Declaracion_Seguimiento { get; set; }
		public DateTime? Fecha_Cierre { get; set; }
		public bool? Cierre { get; set; }
		public int? Id_Municipio_Instituto { get; set; }
	}
}