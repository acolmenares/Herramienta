using System;
using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{
	public class PeticionConsultarPorFecha<T> : IReturn<QueryResponse<T>>, IReturn, ITengoFechaRadicacionDesdeHasta
	//public class PeticionConsultarPorFecha<T> : QueryData<T>,  ITengoFechaRadicacionDesdeHasta
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

