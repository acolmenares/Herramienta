using System;
namespace Herramienta.Modelos.Interfaces
{
	public interface ITransformoFechas
	{
		
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <returns>T.NameyyyyMMddyyyyMMdd</returns>
		string ConvertirEnLlave<T>(ITengoFechaRadicacionDesdeHasta query);
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <returns>urn:Lave</returns>
		string ConvertirEnURN<T>(ITengoFechaRadicacionDesdeHasta query);
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <returns>yyyyMMddyyyyMMdd</returns>
		string ConvertirEnRango(ITengoFechaRadicacionDesdeHasta query);

		string ConvertirLlaveEnRango(string llave);
		string ConvertirLlaveEnT(string llave);
		string ConvertirRangoEnPeriodo(string rango);
		string ConvertirRangoEnAnioMes(string rango);

		//string ConvertirAnioMesEnPeriodo(string anioMes);
		//string ConvertirAnioMesEnMes(string anioMes);
	}
}

