using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Herramienta.Modelos.Interfaces
{
	public interface IFiltroListados <TDestino, TNecesidadBasica>
	{
		List<TNecesidadBasica> FiltrarPor(string nombreFiltro,
		                                TDestino grupo,
		                                List<TNecesidadBasica> listaPorFiltrar);

		List<TNecesidadBasica> FiltrarPor(Expression<Func<TDestino, int>> propiedadNombreFiltro,
		                                  TDestino grupo,
										List<TNecesidadBasica> listaPorFiltrar);


		int ContarFiltradorPor(Expression<Func<TDestino, int>> propiedadNombreFiltro,
		                       TDestino grupo,
		                       List<TNecesidadBasica> listaPorFiltrar);

		int ContarFiltradorPor(string nombreFiltro, 
		                       TDestino grupo,
		                       List<TNecesidadBasica> listaPorFiltrar);

		Dictionary<string, int> ConsultarValoresDeConteo(TDestino destino, 
		                                                 List<TNecesidadBasica> listaPorFiltrar);

		void ActualizarValoresDeConteo(TDestino destino,
		                               List<TNecesidadBasica> listaPorFiltrar);


		List<TDestino> AplicarFiltros(List<TNecesidadBasica> lista);

	}
}
