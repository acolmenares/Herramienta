using System;
using System.Collections.Generic;
using Herramienta.Modelos.Entidades;

namespace Herramienta.Modelos.Interfaces
{
	public interface IHerramientaEnCache
	{
		List<T> Consultar<T>(ITengoFechaRadicacionDesdeHasta rangoFechas, Func<List<T>> funcionConsultar, bool refrescar = false) where T : class;
		void Borrar<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) where T : class;
	}

}

