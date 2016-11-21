using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Herramienta.Modelos.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Herramienta.CapaDatos
{
	public class RepositorioPrincipal:IRepositorioPrincipal
	{
		private const int MaxLengthForInValues = 1800;
		
		private IDbConnection conexion;
		private IDbTransaction transaccion = null;

		public RepositorioPrincipal(IDbConnectionFactory dbConnectionFactory, bool crearTransaccion = false)
		{
			try
			{
				conexion = dbConnectionFactory.Open();
				if (crearTransaccion) Execute(con => transaccion = con.OpenTransaction());
			}
			catch  { }

		}

		public List<T> Consultar<T>(Expression<Func<T, bool>> predicate) where T:IEntidad
		{
			return Execute<List<T>>(con => con.Select(predicate));
		}

		public T Consultar<T>(int id) where T : IEntidad
		{
			return Execute<T>(con => con.SingleById<T>(id));
		}

		public List<T> Consultar<T>() where T : IEntidad
		{
			return Execute<List<T>>(con => con.Select<T>());
		}

		public T ConsultarSimple<T>(Expression<Func<T, bool>> predicate) where T : IEntidad
		{
			return Execute<T>(con => con.Single(predicate));
		}


		public int Actualizar<T>(T data) where T : IEntidad
		{
			int r = 0;
			Execute(con =>
			{
				r = con.Update<T>(data, f => f.Id == data.Id);
			});
			return r;
		}

		public int Actualizar<T>(T data, Expression<Func<T, Object>> onlyFields, Expression<Func<T, bool>> predicate) where T : IEntidad
		{
			int c = 0;
			Execute(con =>
			{
				c = con.UpdateOnly<T>(data, onlyFields, predicate);
			});
			return c;
		}

		public int Actualizar<T>(T data, Expression<Func<T, object>> onlyFields) where T : IEntidad
		{
			int c = 0;
			Execute(con =>
			{
				c = con.UpdateOnly<T>(data, onlyFields, q => q.Id == data.Id);
			});
			return c;
		}

		public int Actualizar<T>(T data, Expression<Func<T, bool>> predicate) where T : IEntidad
		{
			int c = 0;
			Execute(con =>
			{
				var updateOnly = con.From<T>().Where(predicate);
				c = con.UpdateOnly<T>(data, updateOnly);
			});

			return c;
		}


		public void Crear<T>(T data) where T : IEntidad
		{
			Execute(con =>
			{
				con.Insert(data);
				data.Id = int.Parse(con.LastInsertId().ToString());
				Console.WriteLine(data.Id);
			});
		}

		public int Borrar<T>(int id) where T : IEntidad
		{
			int c = 0;
			Execute(con =>
			{
				c = con.DeleteById<T>(id);
			});
			return c;
		}

		public int Borrar<T>(T data) where T : IEntidad
		{
			return Borrar<T>(data.Id);
		}

		public int Borrar<T>(Expression<Func<T, bool>> predicate) where T : IEntidad
		{
			int c = 0;
			Execute(con =>
			{
				c = con.Delete<T>(predicate);
			});
			return c;
		}



		public void IniciarTransaccion()
		{
			if (transaccion != null)
			{
				Execute(con => transaccion = con.OpenTransaction());
			}
		}

		public void FinalizarTransaccion()
		{
			if (transaccion != null)
			{
				transaccion.Commit();
				transaccion.Dispose();
				transaccion = null;
			}
		}

		public void CancelarTransaccion()
		{
			Rollback();
		}

		public void Dispose()
		{
			Console.WriteLine("Repositorio Principal Dispose");
			Rollback();
			Execute(con => con.Dispose());
		}


		private void Rollback()
		{
			if (transaccion != null)
			{
				transaccion.Rollback();
				transaccion.Dispose();
				transaccion = null;
			}
		}

		protected void Execute(Action<IDbConnection> acciones)
		{
			acciones(conexion);
		}

		protected T Execute<T>(Func<IDbConnection, T> acciones)
		{
			return acciones(conexion);
		}


		public List<From> Consultar<From>(ISelector<From> selector) where From : IEntidad
		{
			return Consultar<From, From>(selector);
		}

		public List<Into> Consultar<From, Into>(ISelector<From> selector) where From : IEntidad
		{
			var r = new List<Into>();


			if (selector.InValues != null)
			{
				var fieldForInValue = "";
				if (selector.FieldForInValues != null)
				{
					var fielDefinition = OrmLiteUtils.GetModelDefinition(typeof(From)).GetFieldDefinition<From>(selector.FieldForInValues);
					fieldForInValue =  fielDefinition.FieldName.SqlColumn();
				}

				var ids = Split(selector.InValues);
				foreach (var idList in ids)
				{
					var qr = CrearSqlExpression(selector, includePredicate: false);

					if (selector.Predicate != null)
						qr.Where().Where(selector.Predicate).And(fieldForInValue + " in ({0})", new SqlInValues(idList));
					else
						qr.Where().Where(fieldForInValue + " in ({0})", new SqlInValues(idList));

					Execute(cn =>
					{
						var parcial = cn.Select<Into>(qr); 
						r.AddRange(parcial);
					});
				}
			}
			else
			{
				var qr = CrearSqlExpression(selector, includePredicate:true);
				Execute(cn =>
				{
					r = cn.Select<Into>(qr);
				});
			}
			return r;
		}

		private SqlExpression<From> CrearSqlExpression<From>(ISelector<From> selector, bool includePredicate) where From:IEntidad
		{
			var qr = CrearSqlExpression<From>();

			if (selector.Fields != null)
				qr.Select(selector.Fields);
			if (selector.OrderBy != null)
				qr.OrderBy(selector.OrderBy);
			if (selector.GroupBy != null)
				qr.GroupBy(selector.GroupBy);

			if (includePredicate && selector.Predicate != null)
				qr.Where(selector.Predicate);

			return qr;
		}

		private SqlExpression<T> CrearSqlExpression<T>()
		{
			return conexion.From<T>();
		}

		private static List<List<int>> Split(IEnumerable<int> source, int maxSubItems = MaxLengthForInValues)
		{
			return source
				 .Select((x, i) => new { Index = i, Value = x })
				 .GroupBy(x => x.Index / maxSubItems)
				 .Select(x => x.Select(v => v.Value).ToList())
				 .ToList();
		}

	}
}

