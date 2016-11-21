using System;
using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{
	/*
	public abstract class CrearResponse : IReturn<CreateResponse>, IReturn
	{
	}
	*/

	public abstract class PeticionCrearPorFecha : ITengoFechaRadicacionDesdeHasta, IReturn<ResponseCreate>, IReturn
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

