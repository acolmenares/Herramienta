using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Metadata;

namespace Herramienta.Modelos.Interfaces
{
	public interface IContenedorMetodos<TDestino> where TDestino : INecesidadBasica
	{

		void Registrar(string nombrePropiedad, Func<TablasRango, Personas, object> factoria);

		void Registrar<TTipoDato>(Expression<Func<TDestino, TTipoDato>> propiedad,
		                          Func<TablasRango, Personas, object> factoria);

		TTipo Resolver<TTipo>(Expression<Func<TDestino, TTipo>> propiedad, TablasRango tablasRango, Personas persona);

		object Resolver(string nombrePropiedad, TablasRango tablasRango, Personas persona);

		List<string> Propiedades { get; }

	}
}

