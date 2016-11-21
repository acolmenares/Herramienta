using System;
namespace Herramienta.Modelos.Interfaces
{
	public abstract class NecesidadBasicaCrear : CrearResponse, ITengoFechaRadicacionDesdeHasta
	{
		public DateTime? FechaRadicacionDesde
		{
			get; set;
		}

		public DateTime? FechaRadicacionHasta
		{
			get; set;
		}
	}
}

