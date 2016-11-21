using System.Collections.Generic;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Objetivos;
using ServiceStack;
using Herramienta.Modelos.Extensiones;

namespace Herramienta.CapaNegocios.Gestores
{
	public abstract class NecesidadBasicaGestor<TDatoObjetivo> : INecesidadBasicaGestor<TDatoObjetivo>,
	INecesidadBasicaGestorConsultas<TDatoObjetivo>
		where TDatoObjetivo : class, INecesidadBasica, new()

	{
		readonly IRepositorioNecesidadBasica repositorioDatoObjetivoNecesidadesBasicas;

		readonly IRangoGestorConsultas rangoGestor;
		readonly IHerramientaEnCache cacheClient;
		readonly IGeneradorNecesidadBasica<TDatoObjetivo> generadorDatoNecesidadesBasicas;

		public NecesidadBasicaGestor(
			IHerramientaEnCache cacheClient,
			IRangoGestorConsultas rangoGestor,
			IRepositorioNecesidadBasica repositorioDatoObjetivoNecesidadesBasicas,
			IGeneradorNecesidadBasica<TDatoObjetivo> generadorDatoNecesidadesBasicas
		)
		{
			this.repositorioDatoObjetivoNecesidadesBasicas = repositorioDatoObjetivoNecesidadesBasicas;
			this.cacheClient = cacheClient;
			this.generadorDatoNecesidadesBasicas = generadorDatoNecesidadesBasicas;
			this.rangoGestor = rangoGestor;
		}

		public ResponseDelete Borrar(ITengoFechaRadicacionDesdeHasta peticion)
		{
			cacheClient.Borrar<EstadoAtencion>(peticion);
			repositorioDatoObjetivoNecesidadesBasicas.BorrarDatoObjetivoNecesidadesBasicas<EstadoAtencion>(peticion);
			return new ResponseDelete();
		}

		public ResponseCreate Crear(ITengoFechaRadicacionDesdeHasta peticion)
		{

			repositorioDatoObjetivoNecesidadesBasicas.LanzarCuandoExiste<EstadoAtencion>(peticion);
			var datoObjetivoNecesidadesBasicas = Consultar(peticion).Results;
			repositorioDatoObjetivoNecesidadesBasicas.CrearDatoObjetivoNecesidadesBasicas(peticion, datoObjetivoNecesidadesBasicas);
			return new ResponseCreate();
		}

		public QueryResponse<TDatoObjetivo> Consultar(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{

			var lista = cacheClient.Consultar(rangoFechas,
											  () => ConsultarDesdeHerramienta_O_Generar(rangoFechas));

			return new QueryResponse<TDatoObjetivo> { Results = lista };
		}


		List<TDatoObjetivo> ConsultarDesdeHerramienta_O_Generar(ITengoFechaRadicacionDesdeHasta rango)
		{
			var lista = repositorioDatoObjetivoNecesidadesBasicas.ConsultarDatoObjetivoNecesidadesBasicas<TDatoObjetivo>(rango);
			lista.SiEstaVaciaEntonces(() => lista = GenerarDesdeTablaRangos(rango));
			return lista;
		}


		List<TDatoObjetivo> GenerarDesdeTablaRangos(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var tablasRango = rangoGestor.ConsultarTablasRango(rangoFechas);
			return generadorDatoNecesidadesBasicas.Consultar(tablasRango, Modelos.PersonaTipo.Declarante);
		}

	}
}

