using System;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Entidades
{
	public class SubTablas: IEntidad
    {
        public int Id { get; set; }
        public int? Id_Tabla { get; set; }
        public string Descripcion { get; set; }
        public int? Id_Padre { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public int? orden { get; set; }
        public int? Activo { get; set; }
    }


	/// <summary>
	/// Descripcion declaracion.
	/// Descripcion para: DeclaracionCausasDesplazamiento, DeclaracionPersonasAyuda y similares
	/// </summary>
	public class DescripcionDeclaracion
	{
		public int Id_Declaracion { get; set; }
		public string Descripcion { get; set; }
	}


	public class PersonasPorDeclaracion: IConIdDeclaracion
    {
        public int? Id_Declaracion { get; set; }
        /// <summary>
        /// TFR
        /// </summary>
        public int Cantidad { get; set; }
    }
}
