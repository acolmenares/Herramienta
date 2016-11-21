using System.Collections.Generic;
using Herramienta.Modelos.Metadata;

namespace Herramienta.Modelos.Interfaces
{
	public interface IRepositorioIndicadorNecesidadBasicaConsultas
	{
		List<T> ConsultarIndicadorObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : IIndicadorNecesidadBasica;
	}

	public interface IRepositorioIndicadorNecesidadBasica:IRepositorioIndicadorNecesidadBasicaConsultas
	{
			
		void CrearIndicadorObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas,  List<T> indicadorObjetivo)
			where T : IIndicadorNecesidadBasica;
		
		void BorrarIndicadorObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : IIndicadorNecesidadBasica;
	}

	public interface IRepositorioNecesidadBasicaConsultas
	{
		List<T> ConsultarDatoObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : INecesidadBasica;

		void LanzarCuandoExiste<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
		where T : INecesidadBasica;

		bool ExisteDatoObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : INecesidadBasica;
	}


	public interface IRepositorioNecesidadBasica : IRepositorioNecesidadBasicaConsultas
	{
		void CrearDatoObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas,
													List<T> datoObjetivoNecesidadesBasicas)
			where T : INecesidadBasica;

		void BorrarDatoObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : INecesidadBasica;
	}


	public interface IRepositorioRangoConsultas
	{
		List<Rango> ConsultarRango();
		List<Rango> ConsultarRango(ITengoFechaRadicacionDesdeHasta rangoFechas);
		TablasRango ConsultarTablasRango(ITengoFechaRadicacionDesdeHasta rangoFechas);
		void LanzarExcepcionCuandoNoExisteRango(ITengoFechaRadicacionDesdeHasta rangoFechas);
		void LanzarExcepcionCuandoExisteRango(ITengoFechaRadicacionDesdeHasta peticion);
		bool ExisteRango(ITengoFechaRadicacionDesdeHasta rangoFechas);
		List<T> ConsultarTablaRango<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) where T : IEntidad;
	}

	public interface IRepositorioRango : IRepositorioRangoConsultas
	{
		void CrearRango(ITengoFechaRadicacionDesdeHasta rangoFechas, TablasRango tablasRango);
		void BorrarRango(ITengoFechaRadicacionDesdeHasta rangoFechas);
	}

	public interface IRepositorioHerramienta : IRepositorioRango, IRepositorioRangoConsultas,
	IRepositorioNecesidadBasica, IRepositorioNecesidadBasicaConsultas,
	IRepositorioIndicadorNecesidadBasica, IRepositorioIndicadorNecesidadBasicaConsultas 
	{

	}

}