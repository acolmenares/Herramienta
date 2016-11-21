using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Extensiones
{
	public static class TUtil
	{
		public static void SiEstaVaciaEntonces<T>(this ICollection<T> value, Action ejecutarSi)
		{
			if (value == null || value.Count == 0) ejecutarSi();
		}


		public static void SiEsNullEntonces<T>(this T value, Action ejecutarSi) where T : class
		{
			if (value == null) ejecutarSi();
		}

		public static void EvaluarSiCumpleEntonces<T>(this T value, Func<bool> funcionEvaluar, Action<T> ejecutarSi)
		{
			if (funcionEvaluar()) ejecutarSi(value);
		}

		public static T EvaluarSiCumpleEntonces<T>(this T value, Func<T, bool> funcionEvaluar, Func<T> ejecutarSi)
		{
			return (funcionEvaluar(value)) ? ejecutarSi() : value;

		}

		public static bool EstaVacia<T>(this ICollection<T> value)
		{
			return (value == null || value.Count == 0);
		}


		public static List<TElementoLista> FiltrarPor<TDestino, TElementoLista>(this List<TElementoLista> lista,
		                                                                        IFiltroListados<TDestino, TElementoLista> filtro,
		                                                                        Expression<Func<TDestino, object>> propertyRefExpr,
		                                                                        TDestino grupo)
		{
			var nombreFiltro = PropertyUtil.GetName<TDestino>(propertyRefExpr);
			return filtro.FiltrarPor(nombreFiltro, grupo, lista);
		}



		public static List<TElementoLista> FiltrarPor<TDestino, TElementoLista>(this List<TElementoLista> lista,
		                                                                        IFiltroListados<TDestino, TElementoLista> filtro,
		                                                                        string nombreFiltro,
		                                                                        TDestino grupo)
		{
			return filtro.FiltrarPor(nombreFiltro, grupo, lista);
		}


		public static int ContarFiltradoPor<TDestino, TElementoLista>(this List<TElementoLista> lista,
		                                                              IFiltroListados<TDestino, TElementoLista> filtro,
		                                                              Expression<Func<TDestino, object>> propertyRefExpr,
		                                                              TDestino grupo)
		{
			var nombreFiltro = PropertyUtil.GetName<TDestino>(propertyRefExpr);
			return filtro.ContarFiltradorPor(nombreFiltro,grupo, lista);
		}

		public static int ContarFiltradoPor<TDestino, TElementoLista>(this List<TElementoLista> lista,
																			   IFiltroListados<TDestino, TElementoLista> filtro,
																			   string nombreFiltro,
		                                                              TDestino grupo)
		{
			return filtro.ContarFiltradorPor(nombreFiltro,grupo, lista);

		}
	}
}

