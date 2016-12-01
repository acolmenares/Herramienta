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
using Herramienta.Modelos.Extensiones;
using System.Collections.Generic;

namespace Herramienta.TestIndicadorDosUno
{
	class MainClass
	{

        static void ProbarRecibeRacionUtil()
        {
            var r1 = new RecibeRacion { Periodo = "Q1", AnioMes = "201510" };
            var r2 = new RecibeRacion { Periodo = "Q1", AnioMes = "201511" };
            var r3 = new RecibeRacion { Periodo = "Q1", AnioMes = "201512" };
            var r4 = new RecibeRacion { Periodo = "Q2", AnioMes = "201601" };
            var r5 = new RecibeRacion { Periodo = "Q2", AnioMes = "201602" };
            var r6 = new RecibeRacion { Periodo = "Q2", AnioMes = "201603" };
            var r7 = new RecibeRacion { Periodo = "Q3", AnioMes = "201604" };
            var r8 = new RecibeRacion { Periodo = "Q3", AnioMes = "201604" };
            var r9 = new RecibeRacion { Periodo = "Q3", AnioMes = "201606" };
            var r10 = new RecibeRacion { Periodo = "Q4", AnioMes = "201607" };
            var r11 = new RecibeRacion { Periodo = "Q4", AnioMes = "201608" };
            var r12 = new RecibeRacion { Periodo = "Q4", AnioMes = "201609" };

            var r13 = new RecibeRacion { Periodo = "Q1", AnioMes = "201610" };
            var r14 = new RecibeRacion { Periodo = "Q1", AnioMes = "201611" };
            var r15 = new RecibeRacion { Periodo = "Q1", AnioMes = "201612" };
            var r16 = new RecibeRacion { Periodo = "Q2", AnioMes = "201701" };
            var r17 = new RecibeRacion { Periodo = "Q2", AnioMes = "201702" };
            var r18 = new RecibeRacion { Periodo = "Q2", AnioMes = "201703" };
            var r19 = new RecibeRacion { Periodo = "Q3", AnioMes = "201704" };
            var r20 = new RecibeRacion { Periodo = "Q3", AnioMes = "201704" };
            var r21 = new RecibeRacion { Periodo = "Q3", AnioMes = "201706" };
            var r22 = new RecibeRacion { Periodo = "Q4", AnioMes = "201707" };
            var r23 = new RecibeRacion { Periodo = "Q4", AnioMes = "201708" };
            var r24 = new RecibeRacion { Periodo = "Q4", AnioMes = "201709" };
            

            var lista = new List<RecibeRacion>( new RecibeRacion[] {
                r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12,
                r13, r14, r15, r16, r17, r18, r19, r20, r21, r22, r23, r24
            });


            for (var  i =0; i<24; i++)
            {
                Console.WriteLine("{0} {1}  {2} ",i, r1.TengoPeridoRadicacionAnterior(lista[i]), !r1.TengoPeridoRadicacionAnterior(lista[i])?"OK": "MAL");
            }


            for (var i = 0; i < 20; i++)
            {
                Console.WriteLine("{0} {1}  {2} ", i, r24.TengoPeridoRadicacionAnterior(lista[i]), r24.TengoPeridoRadicacionAnterior(lista[i]) ? "OK" : "MAL");
            }


            Console.ReadLine();


        }
		
		public static void Main(string[] args)
		{
            ProbarRecibeRacionUtil();

            /*
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

            FiltroRecibeRacion filtro
            = new FiltroRecibeRacion(new ContenedorFiltros<RecibeRacion, EstadoAtencion>());

            var gestorNecesidadesBasicas = new EstadoAtencionGestor(
				cache,
				rangoGestor,				repositorioEstadoAtencion,
				generadorEstadosAtencion,
                filtro
			);
            */
            

			//var qr=CargarAtencionDesdeArchivo();
			//Console.WriteLine(qr.Results.Count);

			//var fnoviembre= qr.Results.Where(q => q.MunicipioAtencion == "Florencia"
			//				 && q.FechaRadicacion >= new DateTime(2015, 11, 01)
			//                && q.FechaRadicacion <= new DateTime(2015, 11, 30)).ToList();

			//var fnovdes = fnoviembre.Where(q =>q.TipoDeclarante==DESPLAZADO);

			

			/*var indicador = new EstadoAtencionGestor(cache, rangoGestor, repositorioHerramienta, generadorEstadosAtencion, filtro);
				 

			Console.WriteLine("Iniciado");
			var t1 = DateTime.Now;

			ITengoFechaRadicacionDesdeHasta rangoFechas = new EstadoAtencionConsultar 
			{
				FechaRadicacionDesde= new DateTime(2015,10,1),
				FechaRadicacionHasta= new DateTime(2016, 08, 31),
			};




			var resultados = indicador.Consultar(rangoFechas).Results
			                          .OrderBy(q=>q.MunicipioAtencion).ThenBy(q=>q.AnioMesAtencion)
			                          .ToList();

			Console.Write("Fin {0}", DateTime.Now-t1);*/
            /*
			resultados.ForEach(resultado => {
				Console.WriteLine(resultado.AnioMesAtencion);
				Console.WriteLine(resultado.MunicipioAtencion);
				Console.WriteLine(resultado.Ra);
				Console.WriteLine("-----------");
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
				Console.WriteLine(resultado.CultivosIlicitos);
				 Console.WriteLine(resultado.Excluidos);
				Console.WriteLine(resultado.Elegibles);
				Console.WriteLine("-----------");
				Console.WriteLine(resultado.AtendidosPrimeraEntregaEnElMesDeRadicacion);
				Console.WriteLine(resultado.AtendidosPrimeraEntregaRadicadosEnMesesAnteriores);
				Console.WriteLine(resultado.AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores);
				Console.WriteLine(resultado.TotalAtendidosEnPrimeraEntregaEnElMes);
				Console.WriteLine("-----------");
				Console.WriteLine(resultado.AtendidosPrimeraEntregaEnMesesPosteriores);
				Console.WriteLine(resultado.AtendidosPrimeraEntregaEnPeriodosPosteriores);
				Console.WriteLine(resultado.TotalAtendidosEnPrimeraEntregaRadicadosEnElMes);
				Console.WriteLine("-----------");
				Console.WriteLine("AtendidosSegundaEntrega {0}", resultado.AtendidosEnSegundaEntrega);
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
            */


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



