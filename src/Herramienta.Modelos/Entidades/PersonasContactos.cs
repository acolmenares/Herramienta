using ServiceStack.DataAnnotations;
using System;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Entidades
{
	[Alias("Personas_Contactos")]
	public class PersonasContactos: IEntidad, IConIdPersona
    {
        public int Id { get; set; }
        public int? Id_Persona { get; set; }
        public int? Id_Tipo_Contacto { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public bool? Cierre { get; set; }
    }

}
