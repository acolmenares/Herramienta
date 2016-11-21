using System;
using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{
	public abstract class PeticionBorrarPorFecha: ITengoFechaRadicacionDesdeHasta, IReturn<ResponseDelete>, IReturn
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
	/*
	public abstract class BorrarResponse : IReturn<DeleteResponse>, IReturn
	{
	}
	*/
}
