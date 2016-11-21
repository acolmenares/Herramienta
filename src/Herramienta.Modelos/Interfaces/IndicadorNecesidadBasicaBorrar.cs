using System;
namespace Herramienta.Modelos.Interfaces
{
	public class IndicadorNecesidadBasicaBorrar<TIndicador> : 
	BorrarResponse, ITengoFechaRadicacionDesdeHasta
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

