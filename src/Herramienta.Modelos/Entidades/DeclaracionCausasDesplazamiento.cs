using System;
using Herramienta.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace Herramienta.Modelos.Entidades
{

	[Alias("Declaracion_Causas_Desplazamiento")]
    public class DeclaracionCausasDesplazamiento:IConIdDeclaracion, IDeclaracionConIdSubTabla, IEntidad
    {
        public int Id { get; set; }
        [Alias("Id_Declaracion")]
        public int? Id_Declaracion { get; set; }
        [Alias("Id_Causa_Desplazamiento")]
        public int? Id_SubTabla { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public bool? Cierre { get; set; }
    }

}
