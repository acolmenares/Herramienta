using System;
using System.IO;
using System.Linq;
using Herramienta.CapaDatos;
using Herramienta.CapaNegocios.Gestores;
using Herramienta.CapaNegocios.Reglas;
using Herramienta.CapaNegocios.Valores;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Metadata;
using Herramienta.Modelos.Objetivos;
using Herramienta.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Herramienta.TestIndicadorDosUno
{
	class MainClass
	{
		
		public static void Main(string[] args)
		{
			var appSettings = new AppSettings();

			IDbConnectionFactory dbConnectionFactory = SQLConnectionFactory(appSettings);

			IRepositorioPrincipal repositorio = new RepositorioPrincipal(dbConnectionFactory);
			IRepositorioPrincipalConsultas repositorioPrincipal = new RepositorioPrincipalConsultas(repositorio);
			var transformoFechas = new TransformoFechas();
			var cache = new Cache(new MemoryCacheClient { FlushOnDispose = false }, transformoFechas);

			var repositorioHerramienta = new RepositorioHerramienta(transformoFechas);
			IRepositorioIndicadorNecesidadBasica repositorioIndicador = repositorioHerramienta;
			IRepositorioRango repositorioRango = repositorioHerramienta;
			IRepositorioNecesidadBasica repositorioEstadoAtencion= repositorioHerramienta;
			IRangoGestorConsultas rangoGestor = new RangoGestor(cache, repositorioPrincipal, repositorioRango);

			IGeneradorNecesidadBasica<EstadoAtencion> generadorEstadosAtencion =
				new GeneradorEstadoAtencion(new ContenedorMetodos<EstadoAtencion>());

			var gestorNecesidadesBasicas = new NecesidadBasicaGestor<EstadoAtencion>(
				cache,
				rangoGestor,				repositorioEstadoAtencion,
				generadorEstadosAtencion
			);




			//var qr=CargarAtencionDesdeArchivo();
			//Console.WriteLine(qr.Results.Count);

			//var fnoviembre= qr.Results.Where(q => q.MunicipioAtencion == "Florencia"
			//				 && q.FechaRadicacion >= new DateTime(2015, 11, 01)
			//                && q.FechaRadicacion <= new DateTime(2015, 11, 30)).ToList();

			//var fnovdes = fnoviembre.Where(q =>q.TipoDeclarante==DESPLAZADO);

			FiltroRecibeRacion filtro
			= new FiltroRecibeRacion(new ContenedorFiltros<RecibeRacion, EstadoAtencion>());

			var indicador = new IndicadorNecesidadBasicaGestor<RecibeRacion, EstadoAtencion>
			(cache, repositorioIndicador, gestorNecesidadesBasicas, filtro);
				 

			Console.WriteLine("Iniciado");
			var t1 = DateTime.Now;

			ITengoFechaRadicacionDesdeHasta rangoFechas = new EstadoAtencionConsultar 
			{
				FechaRadicacionDesde= new DateTime(2015,10,1),
				FechaRadicacionHasta= new DateTime(2016, 08, 31),
			};




			var resultados = indicador.Consultar(rangoFechas).Results
			                          .OrderBy(q=>q.Municipio).ThenBy(q=>q.AnioMes)
			                          .ToList();

			Console.Write("Fin {0}", DateTime.Now-t1);

			resultados.ForEach(resultado => {
				Console.WriteLine(resultado.AnioMes);
				Console.WriteLine(resultado.Municipio);
				Console.WriteLine(resultado.Radicados);
				/*Console.WriteLine("-----------");
				Console.WriteLine(resultado.DobleDeclaracion);
				Console.WriteLine(resultado.SuperoLimite);
				Console.WriteLine(resultado.NoAsisistioDosProgramaciones);
				Console.WriteLine(resultado.IncluyoPersonaDeOtroNucleo);
				Console.WriteLine(resultado.Extemporaneidad);
				Console.WriteLine(resultado.FueraDeLaCiudad);
				Console.WriteLine(resultado.NoEsDeplazado);
				Console.WriteLine(resultado.Masivo);
				Console.WriteLine(resultado.AtendidoPorOtraFuente);
				Console.WriteLine(resultado.Intraurbano);
				Console.WriteLine(resultado.CultivosIlicitos);*/
				/* Console.WriteLine(resultado.Excluidos);
				Console.WriteLine(resultado.Elegibles);*/
				/*Console.WriteLine("-----------");
				Console.WriteLine(resultado.AtendidosPrimeraEntregaEnElMesDeRadicacion);
				Console.WriteLine(resultado.AtendidosPrimeraEntregaRadicadosEnMesesAnteriores);
				Console.WriteLine(resultado.AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores);
				Console.WriteLine(resultado.TotalAtendidosEnPrimeraEntregaEnElMes);
				Console.WriteLine("-----------");*/
				/*Console.WriteLine(resultado.AtendidosPrimeraEntregaEnMesesPosteriores);
				Console.WriteLine(resultado.AtendidosPrimeraEntregaEnPeriodosPosteriores);
				Console.WriteLine(resultado.TotalAtendidosEnPrimeraEntregaRadicadosEnElMes);*/
				/*Console.WriteLine("-----------");
				Console.WriteLine("AtendidosSegundaEntrega {0}", resultado.AtendidosEnSegundaEntrega);*/
				Console.WriteLine("-----------");
				Console.WriteLine(resultado.PendientePorAplicarFiltros);
				Console.WriteLine(resultado.PendientePorProgramar);
				Console.WriteLine(resultado.PendienteNoContactado);
				Console.WriteLine(resultado.PendienteProgramadoProximoMes);
				Console.WriteLine(resultado.PendienteProgramadoQueNoAsistio);
				Console.WriteLine("-----------");

				Console.WriteLine(resultado.TotalPendientesPorAtender);
				Console.WriteLine("-----------");
				Console.WriteLine("-----------");
				Console.WriteLine("Radicados {0}", resultado.Excluidos + resultado.TotalAtendidosEnPrimeraEntregaRadicadosEnElMes +
								  resultado.TotalPendientesPorAtender);
				
				Console.WriteLine("Control {0}", resultado.Radicados- (resultado.Excluidos + resultado.TotalAtendidosEnPrimeraEntregaRadicadosEnElMes +
				                                                       resultado.TotalPendientesPorAtender));

				Console.WriteLine("ENTER Para el siguiente");
				Console.ReadLine();

			});


		}

		static QueryResponse<EstadoAtencion> CargarAtencionDesdeArchivo()
		{
			var filename = PathUtils
				.CombinePaths("App_Data", "Herramienta", "2015100120160831", "QueryDeclaracionesEstados.json")
				.MapHostAbsolutePath();


			using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				var atencion = JsonSerializer.DeserializeFromStream<QueryResponse<EstadoAtencion>>(stream);
				return atencion;
			}
		}

		static OrmLiteConnectionFactory SQLConnectionFactory(AppSettings appSettings)
		{
			var conexionBDSeguridad = appSettings.Get("ConexionGestionProyectos", Environment.GetEnvironmentVariable("APP_CONEXION_GP"));

			var dbfactory = new OrmLiteConnectionFactory(conexionBDSeguridad, SqlServerDialect.Provider)
			{
				ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
			};
			return dbfactory;
		}



		static void TestContenedores()
		{
			var contenedor = new ContenedorMetodos<EstadoAtencion>();
			TablasRango tablasRando = new TablasRango();
			Personas persona = new Personas();

			contenedor.Registrar("Nombre", (tr, decl) => "Angel Colmenares");
			contenedor.Registrar("Edad", (tr, decl) => 1);

			var nombre = contenedor.Resolver("Nombre", tablasRando, persona);
			var edad = contenedor.Resolver("Edad", tablasRando, persona);

			Console.WriteLine("{0} {1}", nombre, edad);

			var objNombre = contenedor.Resolver("Nombre", tablasRando, persona);
			var objEdad = contenedor.Resolver("Edad", tablasRando, persona);

			Console.WriteLine("{0} {1}", objNombre, objEdad);
			Console.WriteLine("{0} {1}", objNombre.GetType(), objEdad.GetType());


			var g = new GeneradorEstadoAtencion(new ContenedorMetodos<EstadoAtencion>()); 
			var opnombre = g.Resolver(f => f.PrimerNombre, tablasRando, persona);
			var osnombre = g.Resolver(f => f.SegundoNombre, tablasRando, persona);
			var odireccion = g.Resolver(f => f.Direccion, tablasRando, persona);

			Console.WriteLine("Resueltos {0} {1} {2} ", opnombre, osnombre, odireccion);

			var estadoAtencion = g.CrearDestino(tablasRando, persona);
			Console.WriteLine("Resueltos 0 {0} {1} {2}", estadoAtencion.PrimerNombre, estadoAtencion.SegundoNombre, estadoAtencion.Celular);

			persona.Id = 1;

			estadoAtencion = g.CrearDestino(tablasRando, persona);
			Console.WriteLine("Resueltos 1  {0} {1} {2}", estadoAtencion.PrimerNombre, estadoAtencion.SegundoNombre, estadoAtencion.Celular);

			return;
		}

	}
}


