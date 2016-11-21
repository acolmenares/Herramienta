using System;
namespace Herramienta.Modelos.Interfaces
{
	public interface IConstructorRepositorioPrincipal
	{
		IRepositorioPrincipal Crear(bool crearTransaccion = false);
		void Ejecutar(Action<IRepositorioPrincipal> action, bool crearTransaccion = false) ;
		T Ejecutar<T>(Func<IRepositorioPrincipal, T> func, bool crearTransaccion = false) where T : IEntidad;

		IRepositorioPrincipalConsultas CrearRepositorioPrincipalConsultas(IRepositorioPrincipal repositorioPrincipal);


	}
}

