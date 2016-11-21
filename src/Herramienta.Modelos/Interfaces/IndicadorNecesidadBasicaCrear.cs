using System;
namespace Herramienta.Modelos.Interfaces
{
	public abstract class IndicadorNecesidadBasicaCrear<TIndicador>: CrearResponse, ITengoFechaRadicacionDesdeHasta
		where TIndicador : IIndicadorNecesidadBasica
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

