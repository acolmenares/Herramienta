using System;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Objetivos
{
	public abstract class NecesidadBasica:INecesidadBasica
	{
		public int Id { get; set; }
		public string PeriodoRadicacion { get { return FechaRadicacion.EnPeriodo(); } }
		public string AnioMesRadicacion { get { return FechaRadicacion.EnAnioMes(); } }
		public string MesRadicacion { get { return FechaRadicacion.EnNombreDeMes(); } }

		public string PeriodoAtencion { get { return FechaAtencion.EnPeriodo(); } }
		public string AnioMesAtencion { get { return FechaAtencion.EnAnioMes(); } }
		public string MesAtencion { get { return FechaAtencion.EnNombreDeMes(); } }

		public DateTime? FechaRadicacion { get; set; }
		public DateTime? FechaDesplazamiento { get; set; }
		public DateTime? FechaDeclaracion { get; set; }
		public DateTime? FechaAtencion { get; set; }

		public string Declaracion { get; set; }
		public string Horario { get; set; }
		public string Grupo { get; set; }
		public string Fuente { get; set; }
		public string Regional { get; set; }
		public string MunicipioAtencion { get; set; }
		public string EnLinea { get; set; }
		public string TipoDeclarante { get; set; }
		public string PrimerApellido { get; set; }
		public string SegundoApellido { get; set; }
		public string PrimerNombre { get; set; }
		public string SegundoNombre { get; set; }
		public string Identificacion { get; set; }
		public string TI { get; set; }
		public string Celular { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }
		public string Barrio { get; set; }
		public int TFE { get; set; }
		public int? TFR { get; set; }
	}
}

