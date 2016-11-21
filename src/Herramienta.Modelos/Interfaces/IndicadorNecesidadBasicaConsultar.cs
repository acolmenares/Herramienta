using System;
namespace Herramienta.Modelos.Interfaces
{
	public abstract class IndicadorNecesidadBasicaConsultar<TIndicador>
		: PeticionConsultarPorFecha<TIndicador>, ITengoFechaRadicacionDesdeHasta
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

