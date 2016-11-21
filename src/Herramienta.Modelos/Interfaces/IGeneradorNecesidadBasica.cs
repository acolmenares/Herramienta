using System.Collections.Generic;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Metadata;

namespace Herramienta.Modelos.Interfaces
{
	public interface IGeneradorNecesidadBasica<TDestino> 
		where TDestino: INecesidadBasica, new()
	{
		List<TDestino> Consultar(TablasRango tablasRango);
		List<TDestino> Consultar(TablasRango tablasRango, PersonaTipo personaTipo);
		void ActualizarDestino(TablasRango tablasRango, Personas persona, TDestino destino );
		TDestino CrearDestino(TablasRango tablasRango, Personas persona);
	}

}
