using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Herramienta.Modelos.Extensiones;


namespace Herramienta.Modelos.Interfaces
{
	public class ContenedorFiltros<TIndicador, TNecesidadBasica>:IContenedorFiltros<TIndicador,TNecesidadBasica>
		where TIndicador:IIndicadorNecesidadBasica
		where TNecesidadBasica :INecesidadBasica
	{
		
		readonly Dictionary<string, Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>>> contenedor;

		public ContenedorFiltros()
		{
			contenedor = new Dictionary<string, Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>>>();
		}


		public void Registrar(string nombreFiltro,
											   Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>> funcionFiltrado)
		{
			Agregar_O_Remplazar(nombreFiltro, funcionFiltrado);
		}

		public void Registrar(Expression<Func<TIndicador, int>> propiedadNombreFiltro,
											   Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>> funcionFiltrado)
		{
			Agregar_O_Remplazar(PropertyUtil.GetName<TIndicador,int>(propiedadNombreFiltro), funcionFiltrado);
		}


		public List<string> Propiedades
		{
			get
			{
				return new List<string>(contenedor.Keys);
			}
		}


		public List<TNecesidadBasica> Resolver(string nombreFiltro, TIndicador grupo, List<TNecesidadBasica> listaPorFiltrar)
		{
			return ObtenerFiltroDeConteo(nombreFiltro)(listaPorFiltrar, grupo);
		}


		public List<TNecesidadBasica> Resolver(Expression<Func<TIndicador, object>> propiedadNombreFiltro,
											   TIndicador grupo,
											   List<TNecesidadBasica> listaPorFiltrar)
		{
			return ObtenerFiltroDeConteo(PropertyUtil.GetName<TIndicador>(propiedadNombreFiltro))(listaPorFiltrar, grupo);
		}


		protected Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>> ObtenerFiltroDeConteo(string nombreFiltro)
		{
			return contenedor[nombreFiltro];
		}

		protected void Agregar_O_Remplazar(string nombreFiltro, Func<List<TNecesidadBasica>, TIndicador, List<TNecesidadBasica>> funcionFiltrado)
		{
			contenedor[nombreFiltro] = funcionFiltrado;
		}


	}
}

