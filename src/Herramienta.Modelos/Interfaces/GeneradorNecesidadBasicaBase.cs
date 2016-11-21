using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Metadata;

namespace Herramienta.Modelos.Interfaces
{

	public abstract class GeneradorNecesidadBasicaBase<TDestino>
		: IGeneradorNecesidadBasica<TDestino>
		where TDestino : INecesidadBasica, new()
	{

		readonly IContenedorMetodos<TDestino> contenedor;

		protected GeneradorNecesidadBasicaBase(IContenedorMetodos<TDestino> contenedorMetodos)
		{
			contenedor = contenedorMetodos;
		}

		protected void RegistrarMetodo(string nombrePropiedad, Func<TablasRango, Personas, object> factoria)
		{
			contenedor.Registrar(nombrePropiedad, factoria);
		}

		protected void RegistrarMetodo(Expression<Func<TDestino, object>> propiedad, Func<TablasRango, Personas, object> factoria)
		{
			contenedor.Registrar(propiedad, factoria);
		}

		protected List<string> PropiedadesConMetodo
		{
			get
			{
				return new List<string>(contenedor.Propiedades);
			}
		}


		public TTipo Resolver<TTipo>(Expression<Func<TDestino, TTipo>> propiedad, TablasRango tablasRango, Personas declaracion)
		{
			return contenedor.Resolver<TTipo>(propiedad, tablasRango, declaracion);
		}


		public object Resolver(string nombrePropiedad, TablasRango tablasRango, Personas persona)
		{
			return contenedor.Resolver(nombrePropiedad, tablasRango, persona);
		}



		public virtual void ActualizarDestino(TablasRango tablasRango, Personas persona, TDestino destino)
		{
			PropiedadesConMetodo.ForEach(propiedad =>
			{
				destino.SetPropertyValue(propiedad, Resolver(propiedad, tablasRango, persona));
			});
		}


		public virtual List<TDestino> Consultar(TablasRango tablasRango)
		{
			var lista = new List<TDestino>();
			tablasRango.Personas.ForEach(Persona =>
			{
				var r = CrearDestino(tablasRango, Persona);
				lista.Add(r);
			});
			return lista;
		}

		public virtual List<TDestino> Consultar(TablasRango tablasRango, PersonaTipo personaTipo)
		{
			var tipo = personaTipo.ToString().First().ToString();
			var lista = new List<TDestino>();
			tablasRango.Personas
			           .Where(x => x.Tipo == tipo)
					   .ToList()
					   .ForEach(Persona =>
			{
				var r = CrearDestino(tablasRango, Persona);
				lista.Add(r);
			});

			return lista;
		}


		public virtual TDestino CrearDestino(TablasRango tablasRango, Personas persona)
		{
			var destino = new TDestino();
			PropiedadesConMetodo.ForEach(propiedad =>
			{
				destino.SetPropertyValue(propiedad, Resolver(propiedad, tablasRango, persona));
			});

			return destino;
		}

	}

}
