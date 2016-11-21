using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Interfaces;
using ServiceStack.OrmLite;

namespace Herramienta.CapaDatos
{
	public class RepositorioPrincipalConsultas:IRepositorioPrincipalConsultas
	{
		readonly IRepositorioPrincipal repositorio;

		private static ISelector<T> ConstruirSelectorPorIdsDeclaracion<T>(IEnumerable<int> idsDeclaracion) where T : IConIdDeclaracion, IEntidad
		{
			var selector = new Selector<T>
			{
				InValues = idsDeclaracion,
				FieldForInValues = f => f.Id_Declaracion,
			};

			return selector;
		}

		private static ISelector<T> ConstruirSelectorPorIdsPersonas<T>(IEnumerable<int> idsPersonas) where T : IConIdPersona, IEntidad
		{
			var selector = new Selector<T>
			{
				InValues = idsPersonas,
				FieldForInValues = f => f.Id_Persona,
			};

			return selector;
		}

		private static Expression<Func<T, bool>> ConstruirPredicadoConFechasRadicacion<T>(ITengoFechaRadicacionDesdeHasta modelo) where T:IConFechaRadicacion
		{
			var predicate = PredicateBuilder.True<T>();

			if (modelo.FechaRadicacionDesde.HasValue)
			{
				var f1 = modelo.FechaRadicacionDesde.Value;
				predicate = predicate.And(q => q.Fecha_Radicacion >= f1);
			}

			if (modelo.FechaRadicacionHasta.HasValue)
			{
				var f2 = modelo.FechaRadicacionHasta.Value;
				predicate = predicate.And(q => q.Fecha_Radicacion <= f2);
			}

			return predicate;
		}

		public RepositorioPrincipalConsultas(IRepositorioPrincipal repositorio)
		{
			this.repositorio = repositorio;
		}


		public List<T> Consultar<T>() where T : IEntidad
		{
			return repositorio.Consultar<T>();
		}

		public List<T> Consultar<T>(ITengoFechaRadicacionDesdeHasta peticion) where T : IConFechaRadicacion, IEntidad
		{
			var predicate = ConstruirPredicadoConFechasRadicacion<T>(peticion);
			return repositorio.Consultar<T>(predicate);
		}

		public List<T> ConsultarPorIdsDeclaracion<T>(IEnumerable<int> idsDeclaracion) where T : IConIdDeclaracion, IEntidad
		{
			var selector = ConstruirSelectorPorIdsDeclaracion<T>(idsDeclaracion);
			return repositorio.Consultar(selector);
		}

		public List<T> ConsultarPorIdsPersonas<T>(IEnumerable<int> idsPersonas) where T : IConIdPersona, IEntidad
		{
			var selector = ConstruirSelectorPorIdsPersonas<T>(idsPersonas);
			return repositorio.Consultar(selector);
		}


		public List<PersonasPorDeclaracion> ContarPersonasPorDeclaracion(IEnumerable<int> idsDeclaracion)
		{
			var selector = ConstruirSelectorPorIdsDeclaracion<Personas>(idsDeclaracion);
			selector.Fields = q => new { q.Id_Declaracion, Cantidad = Sql.As(Sql.Count(q.Id), "Cantidad") };
			selector.GroupBy = q => q.Id_Declaracion;
			return repositorio.Consultar<Personas, PersonasPorDeclaracion>(selector);
		}

	}
}

