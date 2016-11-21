using System.Linq;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Objetivos;
using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.CapaNegocios.Gestores
{
	public class RecibeRacionGestor : IndicadorNecesidadBasicaGestor<RecibeRacion, EstadoAtencion>, IRecibeRacionGestor
	{
		public RecibeRacionGestor(IHerramientaEnCache cacheClient,
														 IRepositorioIndicadorNecesidadBasica repositorioIndicador,
														 INecesidadBasicaGestorConsultas<EstadoAtencion> gestorNecesidadesBasicas,
								  IFiltroListados<RecibeRacion, EstadoAtencion> filtro)
			: base(cacheClient, repositorioIndicador, gestorNecesidadesBasicas, filtro)
		{
		}


		public QueryResponse<RecibeRacion> Consultar(RecibeRacionConsultar rangoFechas)
		{
			var lista = base.Consultar(rangoFechas);
			if (!rangoFechas.Municipio.EstaVacia())
			{
				lista.Results = lista.Results.Where(x => x.Municipio == rangoFechas.Municipio).ToList();
				lista.Total = lista.Results.Count;
			}
			return lista;
		}

		public ResponseDelete Borrar(RecibeRacionBorrar peticion)
		{
			return base.Borrar(peticion);
		}

		public ResponseCreate Crear(RecibeRacionCrear peticion)
		{
			return base.Crear(peticion);
		}
	}
}
