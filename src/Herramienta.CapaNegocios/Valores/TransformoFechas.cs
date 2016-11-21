using System;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;
using ServiceStack;

namespace Herramienta.CapaNegocios.Valores
{
	public class TransformoFechas:ITransformoFechas
	{
		
		public string ConvertirEnLlave<T>(ITengoFechaRadicacionDesdeHasta query)
		{
			var type = typeof(T);
			var name = type.Name.StartsWith("QueryResponse`", StringComparison.Ordinal) ? "Query" + type.GetGenericArguments()[0].Name :
				type.Name.StartsWith("List`", StringComparison.Ordinal) ? type.GetGenericArguments()[0].Name :
				type.Name;

			return "{0}{1}".Fmt(name, ConvertirEnRango(query));

		}

		public string ConvertirEnURN<T>(ITengoFechaRadicacionDesdeHasta query)
		{
			return "urn:{0}".Fmt(ConvertirEnLlave<T>(query));
		}


		public string ConvertirEnRango(ITengoFechaRadicacionDesdeHasta query)
		{
			return "{0}{1}".Fmt(
				(query.FechaRadicacionDesde.HasValue ? query.FechaRadicacionDesde.Value : DateTime.MinValue).EnAAAAMMDD(),
				(query.FechaRadicacionHasta.HasValue ? query.FechaRadicacionHasta.Value : DateTime.Now.AddDays(1)).EnAAAAMMDD());
		}

		public string ConvertirLlaveEnRango(string llave)
		{
			return llave.Substring(llave.Length - 16);
		}

		public string ConvertirLlaveEnT(string llave)
		{
			return llave.Substring(0, llave.Length - 16);
		}

		public string ConvertirRangoEnPeriodo(string rango)
		{

			return (ConvertirEnFecha(rango.Substring(8))).EnPeriodo();

		}

		public string ConvertirRangoEnAnioMes(string rango)
		{
			return (ConvertirEnFecha(rango.Substring(8))).EnAnioMes();
		}



		public string EnAAAAMMDD(DateTime? value)
		{
			return value.HasValue ? FormatDate(value.Value) : "";
		}

		protected string FormatDate(DateTime dateTime)
		{
			return dateTime.ToString("yyyyMMdd");
		}

		private DateTime? ConvertirEnFecha(string parcial)
		{
			var sanio = parcial.Substring(0, 4);
			var smes = parcial.Substring(4, 2);
			var sdia = parcial.Substring(6, 2);

			int anio = 0;
			int mes = 0;
			int dia = 0;


			if (int.TryParse(sanio, out anio) && int.TryParse(smes, out mes) && int.TryParse(sdia, out dia))
			{
				return new DateTime(anio, mes, dia);

			}

			return null;
		}


		protected Tuple<DateTime?, DateTime?> ConvertirRangoEnFechas(string rango)
		{
			return Tuple.Create(ConvertirEnFecha(rango.Substring(0, 8)), ConvertirEnFecha(rango.Substring(8, 0)));
		}

	}
}

