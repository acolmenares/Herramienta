using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Herramienta.Modelos.Extensiones;

namespace Herramienta.Modelos.Interfaces
{
	public abstract class FiltroListadosBase<TIndicador, TNecesidadBasica>
		: IFiltroListados<TIndicador, TNecesidadBasica>
		where TIndicador : IIndicadorNecesidadBasica
		where TNecesidadBasica : INecesidadBasica
	{
		readonly IContenedorFiltros<TIndicador, TNecesidadBasica> contenedor;

		protected FiltroListadosBase(IContenedorFiltros<TIndicador, TNecesidadBasica> contenedorFiltros)
		{
			contenedor = contenedorFiltros;
		}


		protected void RegistrarFiltroDeConteo(string nombreFiltro,
											   Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>> funcionFiltrado)
		{
			contenedor.Registrar(nombreFiltro, funcionFiltrado);
		}


		protected void RegistrarFiltroDeConteo(Expression<Func<TIndicador, int>> propiedadNombreFiltro,
											   Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>> funcionFiltrado)
		{
			RegistrarFiltroDeConteo(PropertyUtil.GetName<TIndicador,int>(propiedadNombreFiltro), funcionFiltrado);
		}


		protected List<string> PropiedadesDeConteo
		{
			get
			{
				return new List<string>(contenedor.Propiedades);
			}
		}


		public Dictionary<string, int> ConsultarValoresDeConteo(TIndicador destino, List<TNecesidadBasica> listaPorFiltrar)
		{
			var diccionario = new Dictionary<string, int>();
			PropiedadesDeConteo.ForEach(propiedad =>
			{
				diccionario.Add(propiedad, ContarFiltradorPor(propiedad, destino, listaPorFiltrar));

			});
			return diccionario;
		}


		public void ActualizarValoresDeConteo(TIndicador destino, List<TNecesidadBasica> listaPorFiltrar)
		{
			PropiedadesDeConteo.ForEach(propiedad =>
			{
				destino.SetPropertyValue(propiedad, ContarFiltradorPor(propiedad, destino, listaPorFiltrar));

			});
		}


		public List<TNecesidadBasica> FiltrarPor(string nombreFiltro, TIndicador grupo, List<TNecesidadBasica> listaPorFiltrar)
		{
			return contenedor.Resolver(nombreFiltro, grupo,listaPorFiltrar);
		}


		public List<TNecesidadBasica> FiltrarPor(Expression<Func<TIndicador, int>> propiedadNombreFiltro,
											   TIndicador grupo,
											   List<TNecesidadBasica> listaPorFiltrar)
		{
			return contenedor.Resolver(PropertyUtil.GetName<TIndicador,int>(propiedadNombreFiltro), grupo, listaPorFiltrar);
		}



		public int ContarFiltradorPor(Expression<Func<TIndicador, int>> propiedadNombreFiltro,
									  TIndicador grupo,
									  List<TNecesidadBasica> listaPorFiltrar)
		{
			return ContarFiltradorPor(PropertyUtil.GetName<TIndicador,int>(propiedadNombreFiltro), grupo, listaPorFiltrar);
		}


		public int ContarFiltradorPor(string nombreFiltro, TIndicador grupo, List<TNecesidadBasica> listaPorFiltrar)
		{
			return contenedor.Resolver(nombreFiltro, grupo, listaPorFiltrar).Count;
		}

		public abstract List<TIndicador> AplicarFiltros(List<TNecesidadBasica> lista);

	}

}
