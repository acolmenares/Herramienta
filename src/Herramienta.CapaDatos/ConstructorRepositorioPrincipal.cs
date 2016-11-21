using System;
using Herramienta.Modelos.Interfaces;
using ServiceStack.Data;

namespace Herramienta.CapaDatos
{
	public class ConstructorRepositorioPrincipal:IConstructorRepositorioPrincipal
	{
		
		public IDbConnectionFactory ConnectionFactory { get; set; }

		public IRepositorioPrincipal Crear(bool crearTransaccion = false)
		{
			return new RepositorioPrincipal(ConnectionFactory, crearTransaccion);
		}

		public IRepositorioPrincipalConsultas CrearRepositorioPrincipalConsultas(IRepositorioPrincipal repositorioPrincipal)
		{
			return new RepositorioPrincipalConsultas(repositorioPrincipal);
		}

		public void Ejecutar(Action<IRepositorioPrincipal> action, bool crearTransaccion = false)
		{
			using (var rp = Crear(crearTransaccion))
			{
				action(rp);
			}
		}

		public T Ejecutar<T>(Func<IRepositorioPrincipal, T> func, bool crearTransaccion = false) where T : IEntidad
		{
			var r = default(T);
			using (var rp = Crear(crearTransaccion))
			{
				r = func(rp);
			}
			return r;
		}

	}
}
