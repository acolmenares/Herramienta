using ServiceStack.DataAnnotations;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Entidades
{
	[Alias("Declaracion_Personas_Ayuda")]
    public class DeclaracionPersonasAyuda: IConIdDeclaracion, IDeclaracionConIdSubTabla, IEntidad
    {
        public int Id { get; set; }
        [Alias("Id_Declaracion")]
        public int? Id_Declaracion { get; set; }
        [Alias("Id_Personas_Ayuda")]
        public int? Id_SubTabla { get; set; }
        [Alias("Id_Tipo_Entrega")]
        public int? TipoEntregaId { get; set; }
        [Alias("Id_Declaracion_Seguimiento")]
        public int? DeclaracionSeguimientoId { get; set; }
    }
}
