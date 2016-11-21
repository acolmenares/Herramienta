using System;
using System.Collections.Generic;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Metadata
{
	public class Rango:ITengoFechaRadicacionDesdeHasta
	{
		public Rango()
		{
		}

		/// <summary>
		/// yyyyMMddyyyyMMdd DesdeHasta
		/// </summary>
		public string Nombre { get; set; }
		/// <summary>
		/// Ultimo Periodo del Rango : Q1 || Q2 || Q3 || Q4
		/// </summary>
		public string Periodo { get; set; }
		/// <summary>
		/// Ultimo AñoMes del Rango yyyyMM
		/// </summary>
		public string AnioMes { get; set; }

		public List<RegionalObjetivo> Regionales { get; set; }
		public List<string> Municipios { get; set; }

		public string Texto { get { return string.Format("{0} {1}", Periodo, AnioMes); } }

		public DateTime? FechaRadicacionDesde
		{
			get;set;
		}

		public DateTime? FechaRadicacionHasta
		{
			get; set;
		}
	}
}

