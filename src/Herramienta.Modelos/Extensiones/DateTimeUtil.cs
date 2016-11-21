using System;
namespace Herramienta.Modelos.Extensiones
{
	public static class DateTimeUtil
	{
		public static readonly string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

		public static readonly string formatoAnioMes = "yyyyMM";
		public static readonly string formatoAAAAMMDD = "yyyyMMdd";

		public static string EnAnioMes(this DateTime? value)
		{
			return value.HasValue ? value.Value.EnAnioMes() : string.Empty;
		}

		public static string EnPeriodo(this DateTime? value)
		{
			return value.HasValue ? value.Value.EnPeriodo() : "Q";
		}

		public static string EnNombreDeMes(this DateTime? value)
		{
			return value.HasValue ? value.Value.EnNombreDeMes() : string.Empty;
		}

		public static string EnAAAAMMDD(this DateTime? value)
		{
			return value.HasValue ? value.Value.EnAAAAMMDD() : "";
		}

		public static string EnAnioMes(this DateTime value)
		{
			return value.ToString(formatoAnioMes);
		}

		public static string EnPeriodo(this DateTime value)
		{
			return string.Format("Q{0}", (((value.Month + 2) % 12) + 3) / 3);
		}

		public static string EnNombreDeMes(this DateTime value)
		{
			return meses[value.Month - 1];
		}

		public static string EnAAAAMMDD(this DateTime value)
		{
			return value.ToString(formatoAAAAMMDD);
		}



	}
}

