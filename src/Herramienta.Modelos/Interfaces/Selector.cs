using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Herramienta.Modelos.Interfaces
{
	public class Selector<From> : ISelector<From> where From : IEntidad
	{
		public Expression<Func<From, object>> Fields { get; set; }
		public Expression<Func<From, bool>> Predicate { get; set; }
		public IEnumerable<int> InValues { get; set; }
		public Expression<Func<From, object>> FieldForInValues { get; set; }
		public Expression<Func<From, object>> OrderBy { get; set; }
		public Expression<Func<From, object>> GroupBy { get; set; }
	}
}

