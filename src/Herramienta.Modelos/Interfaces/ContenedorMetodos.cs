using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Metadata;

namespace Herramienta.Modelos.Interfaces
{

	public class ContenedorMetodos<TDestino>:IContenedorMetodos<TDestino>
		where TDestino:INecesidadBasica
	{
		readonly Dictionary<string, object> contenedor;

		public ContenedorMetodos()
		{
			contenedor = new Dictionary<string, object>();
		}


		public void Registrar(string nombrePropiedad, Func<TablasRango, Personas, object> factoria)
		{
			contenedor.ContainsKey(nombrePropiedad).SiCumpleEntonces(() => contenedor.Remove(nombrePropiedad));
			contenedor.Add(nombrePropiedad, factoria);
		}

		public void Registrar<TTipoDato>(Expression<Func<TDestino, TTipoDato>> propiedad, Func<TablasRango, Personas, object> factoria)
		{
			var nombrePropiedad = PropertyUtil.GetName<TDestino,TTipoDato>(propiedad);
			Registrar(nombrePropiedad, factoria);
		}

		public TTipo Resolver<TTipo>(Expression<Func<TDestino, TTipo>> propiedad, TablasRango tablasRango, Personas persona)
		{
			var nombrePropiedad = PropertyUtil.GetName<TDestino,TTipo>(propiedad);
			return (TTipo) Resolver(nombrePropiedad, tablasRango, persona);
		}


		public object Resolver(string nombrePropiedad, TablasRango tablasRango, Personas persona)
		{
			object factoria = contenedor[nombrePropiedad];
			return ((Func<TablasRango, Personas, object>)factoria).Invoke(tablasRango, persona);
		}

		public List<string> Propiedades
		{
			get
			{
				return new List<string>(contenedor.Keys);
			}
		}

	}



	internal class LlaveContenedor
	{
		public LlaveContenedor(string nombrePropiead, Type tipoPropiedad)
		{
			NombrePropiedad = nombrePropiead;
			TipoPropiedad = tipoPropiedad;
		}

		public string NombrePropiedad { get; private set; }
		public Type TipoPropiedad { get; private set; }


		public bool Equals(LlaveContenedor other)
		{
			return Equals(this, other);
		}

		public override bool Equals(object obj)
		{
			return Equals(this, obj as LlaveContenedor);
		}

		public static bool Equals(LlaveContenedor obj1, LlaveContenedor obj2)
		{
			if (Object.Equals(null, obj1) ||
				Object.Equals(null, obj2))
				return false;

			return obj1.TipoPropiedad == obj2.TipoPropiedad &&
					   obj1.NombrePropiedad == obj2.NombrePropiedad;
		}

		public override int GetHashCode()
		{
			int hash = NombrePropiedad.GetHashCode();
			hash ^= TipoPropiedad.GetHashCode();
			return hash;
		}
	}
}

