using System;
namespace Herramienta.Modelos.Interfaces
{
	public interface ITengoFechaRadicacionDesdeHasta
	{
		DateTime? FechaRadicacionDesde { get; set; }
		DateTime? FechaRadicacionHasta { get; set; }
	}
}

