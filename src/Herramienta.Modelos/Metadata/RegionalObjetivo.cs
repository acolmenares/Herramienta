using System;
using System.Collections.Generic;

namespace Herramienta.Modelos.Metadata
{
	public class RegionalObjetivo
	{
		public RegionalObjetivo()
		{
			Municipios = new List<MunicipioObjectivo>();
		}
		public string Nombre { get; set; }
		public List<MunicipioObjectivo> Municipios { get; set; }
	}

	public class MunicipioObjectivo
	{
		public MunicipioObjectivo()
		{
			AniosMesesRadicacion = new List<AnioMesObjetivo>();
			AniosMesesAtencion = new List<AnioMesObjetivo>();

		}
		public string Regional { get; set; }
		public string Nombre { get; set; }
		public List<AnioMesObjetivo> AniosMesesRadicacion { get; set; }
		public List<AnioMesObjetivo> AniosMesesAtencion { get; set; }

	}
	public class AnioMesObjetivo
	{
		public string Regional { get; set; }
		public string Municipio { get; set; }
		public string Nombre { get; set; }
	}
}

