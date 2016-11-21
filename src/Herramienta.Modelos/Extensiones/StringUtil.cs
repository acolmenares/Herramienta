using System;
namespace Herramienta.Modelos.Extensiones
{
	public static class StringUtil
	{
		public static bool EstaVacia(this string value)
		{
			return string.IsNullOrEmpty(value);
		}

		/// <summary>
		/// Convierte AnioMes AAAAMM en  NombreDeMes
		/// </summary>
		/// <returns>Nombre del Mes que corresponde a aniomes AAAAMM.</returns>
		/// <param name="aniomes">Aniomes.</param>
		public static string AnioMesEnMes(this string aniomes)
		{
			int probarEntero = 0;
			return (int.TryParse(aniomes, out probarEntero) && aniomes.Length == 6) ?
				(new DateTime(int.Parse(aniomes.Substring(0, 4)), int.Parse(aniomes.Substring(4, 2)), 1)).EnNombreDeMes() :
																										 string.Empty;
		}

		/// <summary>
		/// Convierte AnioMes AAAAMM en  Q{n} n= numero del periodo 
		/// </summary>
		/// <returns>Perido Q{n} que corresponse a aniomes en AAAAMM</returns>
		/// <param name="aniomes">Aniomes.</param>

		public static string AnioMesEnPeriodo(this string aniomes)
		{
			int probarEsEntero = 0;
			return (int.TryParse(aniomes, out probarEsEntero) && aniomes.Length == 6)
				?
				(new DateTime(int.Parse(aniomes.Substring(0, 4)), int.Parse(aniomes.Substring(4, 2)), 1)).EnPeriodo()
																											 : string.Empty;
		}

	}
}

