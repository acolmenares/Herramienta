using System.Collections.Generic;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Metadata;
using Herramienta.Modelos.Peticiones;
using ServiceStack;

namespace Herramienta.CapaNegocios.Gestores
{
	public class RangoGestor : IRangoGestor,IRangoGestorConsultas
	{
		readonly IRepositorioRango repositorioRango;
		readonly IRepositorioPrincipalConsultas repositorioPrincipal;
		readonly IHerramientaEnCache cacheClient;

		public RangoGestor(IHerramientaEnCache cacheClient, IRepositorioPrincipalConsultas repositorioPrincipal,
		                   IRepositorioRango repositorioRango)

		{
			this.repositorioRango = repositorioRango;
			this.repositorioPrincipal = repositorioPrincipal;
			this.cacheClient = cacheClient;
		}

		public ResponseDelete Borrar(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			repositorioRango.LanzarExcepcionCuandoNoExisteRango(rangoFechas);
			repositorioRango.BorrarRango(rangoFechas);
			return new ResponseDelete();
		}

		public QueryResponse<Rango> Consultar(RangoConsultar rangoFechas)
		{
			var rango = repositorioRango.ConsultarRango();
			return new QueryResponse<Rango>
			{
				Results = rango,
				Total = rango.Count

			};
		}

		public ResponseCreate Crear(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			repositorioRango.LanzarExcepcionCuandoExisteRango(rangoFechas);
			TablasRango tr = ConsultarTablasRango(rangoFechas); //repositorioRango.ConsultarTablasRango(peticion);
			repositorioRango.CrearRango(rangoFechas, tr);
			return new ResponseCreate();
		}


		public TablasRango ConsultarTablasRango(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var tr = new TablasRango();
			tr.SubTablas = Consultar<SubTablas>(rangoFechas);
			tr.Programacion = Consultar<Programacion>(rangoFechas);
			tr.Declaracion = ConsultarPorRangoFechaRadicacion<Declaracion>(rangoFechas);
			tr.DeclaracionEstados = Consultar<DeclaracionEstados>(rangoFechas, tr.Declaracion);
			tr.Personas = Consultar<Personas>(rangoFechas, tr.Declaracion);
			tr.PersonasContactos = Consultar<PersonasContactos>(rangoFechas, tr.Personas);

			tr.PersonasEducacion = Consultar<PersonasEducacion>(rangoFechas, tr.Personas);
			tr.PersonasRegimenSalud = Consultar<PersonasRegimenSalud>(rangoFechas, tr.Personas);
			tr.DeclaracionBienes = Consultar<DeclaracionBienes>(rangoFechas, tr.Declaracion);
			tr.DeclaracionCausasDesplazamiento = Consultar<DeclaracionCausasDesplazamiento>(rangoFechas, tr.Declaracion);
			tr.DeclaracionDaniosFamilia = Consultar<DeclaracionDaniosFamilia>(rangoFechas, tr.Declaracion);

			tr.DeclaracionFuentesIngreso = Consultar<DeclaracionFuentesIngreso>(rangoFechas, tr.Declaracion);
			tr.DeclaracionObtencionAgua = Consultar<DeclaracionObtencionAgua>(rangoFechas, tr.Declaracion);
			tr.DeclaracionPersonasAyuda = Consultar<DeclaracionPersonasAyuda>(rangoFechas, tr.Declaracion);
			tr.DeclaracionPsicosocial = Consultar<DeclaracionPsicosocial>(rangoFechas, tr.Declaracion);

			return tr;
		}

		List<T> Consultar<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : class, IEntidad
		{
			return cacheClient.Consultar(rangoFechas, () => ConsultarDeHerramienta_O_Repositorio<T>(rangoFechas));
		}

		List<T> Consultar<T>(ITengoFechaRadicacionDesdeHasta rangoFechas, List<Personas> personas)
			where T : class, IEntidad, IConIdPersona
		{
			return cacheClient.Consultar(rangoFechas, () => ConsultarPorIdsPersonasDeHerramienta_O_Repositorio<T>(rangoFechas, personas));
		}

		List<T> Consultar<T>(ITengoFechaRadicacionDesdeHasta rangoFechas, List<Declaracion> declaracion)
			where T : class, IEntidad, IConIdDeclaracion
		{
			return cacheClient.Consultar(rangoFechas, () => ConsultarPorIdsDeclaracionDeHerramienta_O_Repositorio<T>(rangoFechas, declaracion));
		}

		List<T> ConsultarPorRangoFechaRadicacion<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : class, IEntidad, IConFechaRadicacion
		{
			return cacheClient.Consultar(rangoFechas, () => ConsultarPorRangoFechaRadicacionDeHerramiento_O_Repositorio<T>(rangoFechas));
		}

		List<T> ConsultarDeHerramienta_O_Repositorio<T>(ITengoFechaRadicacionDesdeHasta rango) where T : IEntidad
		{
			var lista = repositorioRango.ConsultarTablaRango<T>(rango);
			lista.SiEstaVaciaEntonces(() => lista = repositorioPrincipal.Consultar<T>());
			return lista;
		}

		List<T> ConsultarPorIdsDeclaracionDeHerramienta_O_Repositorio<T>(ITengoFechaRadicacionDesdeHasta rangoFechas,
		                                                                 List<Declaracion> declaracion)
			where T : IEntidad, IConIdDeclaracion
		{
			var lista = repositorioRango.ConsultarTablaRango<T>(rangoFechas);
			lista.SiEstaVaciaEntonces(() =>
			{
				var idsDeclaracion = declaracion.ConvertAll(q => q.Id);
				lista = repositorioPrincipal.ConsultarPorIdsDeclaracion<T>(idsDeclaracion);
			});
			return lista;
		}

		List<T> ConsultarPorIdsPersonasDeHerramienta_O_Repositorio<T>(ITengoFechaRadicacionDesdeHasta rangoFechas,
		                                                              List<Personas> personas)
			where T : IEntidad, IConIdPersona
		{
			var lista = repositorioRango.ConsultarTablaRango<T>(rangoFechas);
			lista.SiEstaVaciaEntonces(() =>
			{
				var idsPersonas = personas.ConvertAll(q => q.Id);
				lista = repositorioPrincipal.ConsultarPorIdsPersonas<T>(idsPersonas);
			});
			return lista;
		}


		List<T> ConsultarPorRangoFechaRadicacionDeHerramiento_O_Repositorio<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : IEntidad, IConFechaRadicacion
		{
			var lista = repositorioRango.ConsultarTablaRango<T>(rangoFechas);
			lista.SiEstaVaciaEntonces(() =>
			{
				lista = repositorioPrincipal.Consultar<T>(rangoFechas);
			});
			return lista;
		}

	}
}

