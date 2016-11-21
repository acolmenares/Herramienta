using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Herramienta.Modelos.Interfaces
{
	public interface IContenedorFiltros<TIndicador, TNecesidadBasica>
		where TIndicador : IIndicadorNecesidadBasica
		where TNecesidadBasica : INecesidadBasica
	{
		void Registrar(string nombreFiltro,
					   Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>> funcionFiltrado);

		void Registrar(Expression<Func<TIndicador, int>> propiedadNombreFiltro,
					   Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>> funcionFiltrado);

		List<string> Propiedades
		{
			get;
		}

		List<TNecesidadBasica> Resolver(string nombreFiltro, TIndicador grupo, List<TNecesidadBasica> listaPorFiltrar);

		List<TNecesidadBasica> Resolver(Expression<Func<TIndicador, object>> propiedadNombreFiltro,
											   TIndicador grupo,
										List<TNecesidadBasica> listaPorFiltrar);
	}
}

