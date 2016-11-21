using System.Collections.Generic;
using Herramienta.Modelos.Interfaces;
using ServiceStack;

namespace Herramienta.CapaNegocios.Gestores
{
	public abstract class IndicadorNecesidadBasicaGestor<TIndicador, TNecesidad>
		: IIndicadorNecesidadBasicaGestor<TIndicador>
		where TIndicador : class, IIndicadorNecesidadBasica
		where TNecesidad : INecesidadBasica
	{
		readonly IFiltroListados<TIndicador, TNecesidad> filtro;
		readonly IRepositorioIndicadorNecesidadBasica repositorioIndicador;
		readonly IHerramientaEnCache cacheClient;
		readonly INecesidadBasicaGestorConsultas<TNecesidad> gestorNecesidadesBasicas;

		protected IndicadorNecesidadBasicaGestor(IHerramientaEnCache cacheClient,
														 IRepositorioIndicadorNecesidadBasica repositorioIndicador,
														 INecesidadBasicaGestorConsultas<TNecesidad> gestorNecesidadesBasicas,
														 IFiltroListados<TIndicador, TNecesidad> filtro)
		{
			this.filtro = filtro;
			this.repositorioIndicador = repositorioIndicador;
			this.cacheClient = cacheClient;
			this.gestorNecesidadesBasicas = gestorNecesidadesBasicas;
		}

		public ResponseDelete Borrar(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			cacheClient.Borrar<TIndicador>(rangoFechas);
			repositorioIndicador.BorrarIndicadorObjetivoNecesidadesBasicas<TIndicador>(rangoFechas);
			return new ResponseDelete();
		}

		public virtual QueryResponse<TIndicador> Consultar(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var listaIndicador =
				cacheClient.Consultar(rangoFechas,
									  () => ConsultarDatosObjetivo_Y_AplicarFitros(rangoFechas));

			return new QueryResponse<TIndicador>
			{
				Results = listaIndicador,
				Total = listaIndicador.Count
			};

		}


		public ResponseCreate Crear(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var indicador = Consultar(rangoFechas).Results;
			repositorioIndicador.CrearIndicadorObjetivoNecesidadesBasicas(rangoFechas, indicador);
			return new ResponseCreate();
		}

		List<TIndicador> ConsultarDatosObjetivo_Y_AplicarFitros(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var datosObjectivo = gestorNecesidadesBasicas.Consultar(rangoFechas).Results;
			return filtro.AplicarFiltros(datosObjectivo);
		}

	}
}
