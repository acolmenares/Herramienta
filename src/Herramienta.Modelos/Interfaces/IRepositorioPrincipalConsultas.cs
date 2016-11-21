using System.Collections.Generic;
using Herramienta.Modelos.Entidades;

namespace Herramienta.Modelos.Interfaces
{
	public interface IRepositorioPrincipalConsultas
	{
		List<T> Consultar<T>() where T : IEntidad;
		List<T> Consultar<T>(ITengoFechaRadicacionDesdeHasta peticion) where T : IConFechaRadicacion, IEntidad;
		List<T> ConsultarPorIdsDeclaracion<T>(IEnumerable<int> idsDeclaracion) where T : IConIdDeclaracion, IEntidad;
		List<T> ConsultarPorIdsPersonas<T>(IEnumerable<int> idsPersonas) where T : IConIdPersona, IEntidad;

		List<PersonasPorDeclaracion> ContarPersonasPorDeclaracion(IEnumerable<int> idsDeclaracion);
	}
}

