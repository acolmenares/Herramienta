using System;
namespace Herramienta.Modelos.Interfaces
{
	public abstract class NecesidadBasicaConsultar<TNecesidadBasica>:
	PeticionConsultarPorFecha<TNecesidadBasica>, ITengoFechaRadicacionDesdeHasta 
		where TNecesidadBasica:INecesidadBasica
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

