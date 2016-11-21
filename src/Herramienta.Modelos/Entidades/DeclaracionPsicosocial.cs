using Herramienta.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace Herramienta.Modelos.Entidades
{
	[Alias("Declaracion_Psicosocial")]
	public class DeclaracionPsicosocial: IConIdDeclaracion, IEntidad
	{
		public int Id { get; set; }
		public int? Id_Declaracion { get; set; }
		public int? Id_Emocion { get; set; }
		public int? Dato_Usted { get; set; }
		public int? Dato_Familia { get; set; }
		public int? Id_Tipo_Entrega { get; set; }
		public int? Id_Declaracion_Seguimiento { get; set; }
	}
}

