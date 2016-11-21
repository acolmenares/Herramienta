using System;
using System.Collections.Generic;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;
using ServiceStack.Caching;

namespace Herramienta.CapaDatos
{
	public class Cache:IHerramientaEnCache
	{
		readonly ICacheClient cacheClient;
		readonly ITransformoFechas transformoFechas;

		string GetKey<T>(ITengoFechaRadicacionDesdeHasta query)
		{
			return transformoFechas.ConvertirEnLlave<T>(query);
		}

		public Cache(ICacheClient cacheClient, ITransformoFechas transformoFechas )
		{
			this.cacheClient = cacheClient;
			this.transformoFechas = transformoFechas;
		}

		public List<T> Consultar<T>(ITengoFechaRadicacionDesdeHasta rangoFechas, Func<List<T>> funcionConsultar, bool refrescar = false) where T : class
		{
			var key = GetKey<T>(rangoFechas);
			var r = new List<T>();

			(!refrescar).SiCumpleEntonces(() =>
			{
				r = cacheClient.Get<List<T>>(key);
			});

			r.SiEsNullEntonces(() =>
			{
				r = funcionConsultar();
				cacheClient.Set(key, r, TimeSpan.FromMinutes(10));
			});

			return r;
		}

		public void Borrar<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) where T : class
		{
			var key = GetKey<T>(rangoFechas);
			cacheClient.Remove(key);
		}
	}
}

