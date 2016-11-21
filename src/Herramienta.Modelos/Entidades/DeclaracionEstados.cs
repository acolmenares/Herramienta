using System;
using Herramienta.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace Herramienta.Modelos.Entidades
{
	[Alias("Declaracion_Estados")]
    public class DeclaracionEstados: IConIdDeclaracion, IEntidad
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Id_Declaracion { get; set; }
        public int? Id_Tipo_Estado { get; set; }
        public int? Id_Como_Estado { get; set; }
        public int? Id_Programa { get; set; }
        public int? Id_Asistio { get; set; }
        public int? Id_Motivo_NoAtencion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public bool? Cierre { get; set; }

    }
}
