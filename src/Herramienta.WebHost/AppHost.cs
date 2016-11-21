using System;
using Funq;
using Herramienta.CapaDatos;
using Herramienta.CapaNegocios.Gestores;
using Herramienta.CapaNegocios.Reglas;
using Herramienta.CapaNegocios.Valores;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Objetivos;
using Herramienta.Modelos.Peticiones;
using Herramienta.Servicios;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;

namespace Herramienta.WebHost
{
	public class AppHost : AppHostBase
	{
		public AppHost() : base("Herramienta de Indicadores", typeof(RangoServicio).Assembly) { }

		public override void Configure(Container container)
		{
			SetConfig(new HostConfig
			{
				DebugMode = true,
				HandlerFactoryPath = "hr-api",
				GlobalResponseHeaders =
					{
						{ "Access-Control-Allow-Origin", "*" },
						{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH" },
					},
				DefaultContentType = "json"
			});

			ConfigureRoutes();

			Plugins.Add(new CorsFeature());
			Plugins.Add(new SessionFeature()); // TODO : PONER REDIS AQUI!

			Plugins.Add(new AuthFeature(() => new AuthUserSession(),
										new IAuthProvider[] { new BasicAuthProvider() },
										"~/metadata"
									   ));

			var appSettings = new AppSettings();
			var connectionFactory = SQLConnectionFactory(appSettings);
			container.Register<IDbConnectionFactory>(connectionFactory);
			container.Register<ICacheClient>(new MemoryCacheClient { FlushOnDispose = false });

			container.RegisterAs<TransformoFechas, ITransformoFechas>();//.ReusedWithin(ReuseScope.Request);

			container.Register<IHerramientaEnCache>(
				c => new Cache(c.Resolve<ICacheClient>(),
											c.Resolve<ITransformoFechas>()));

			container.Register<IRepositorioPrincipal>(
				c => new RepositorioPrincipal(c.Resolve<IDbConnectionFactory>()))
					 .ReusedWithin(ReuseScope.Request); ;

			container.Register<IRepositorioPrincipalConsultas>(
				c => new RepositorioPrincipalConsultas(c.Resolve<IRepositorioPrincipal>()))
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IRepositorioHerramienta>(
				c => new RepositorioHerramienta(c.Resolve<ITransformoFechas>(),
														 "Herramienta"))
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IRangoGestor>(
				c => new RangoGestor(c.Resolve<IHerramientaEnCache>(),
									 c.Resolve<IRepositorioPrincipalConsultas>(),
				                     c.Resolve<IRepositorioHerramienta>()))
					 .ReusedWithin(ReuseScope.Request);


			container.Register<IGeneradorNecesidadBasica<EstadoAtencion>>(
				c => new GeneradorEstadoAtencion(new ContenedorMetodos<EstadoAtencion>()))
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IFiltroListados<RecibeRacion, EstadoAtencion>>(
				c => new FiltroRecibeRacion(new ContenedorFiltros<RecibeRacion, EstadoAtencion>()))
			.ReusedWithin(ReuseScope.Request);

			container.Register<IEstadoAtencionGestor>(
				c => new EstadoAtencionGestor(c.Resolve<IHerramientaEnCache>(),
											 c.Resolve<IRangoGestor>(),
											 c.Resolve<IRepositorioHerramienta>(),
											 c.Resolve<IGeneradorNecesidadBasica<EstadoAtencion>>(),
											 c.Resolve<IFiltroListados<RecibeRacion, EstadoAtencion>>()))
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IRecibeRacionGestor>(
				c => new RecibeRacionGestor(
					c.Resolve<IHerramientaEnCache>(),
					c.Resolve<IRepositorioHerramienta>(),
					c.Resolve<IEstadoAtencionGestor>(),
					c.Resolve<IFiltroListados<RecibeRacion, EstadoAtencion>>()))
			         .ReusedWithin(ReuseScope.Request);
			//var gestor = container.Resolve<IIndicadorNecesidadBasicaGestor<RecibeRacion>>();
			//Console.WriteLine(gestor);

		}

		void ConfigureRoutes()
		{
			
			Routes.Add<RangoConsultar>("/rango/consultar", ApplyTo.Get);
			Routes.Add<RangoCrear>("/rango/crear", ApplyTo.Post);
			Routes.Add<RangoBorrar>("/rango/borrar", ApplyTo.Post);
			Routes.Add<EstadoAtencionConsultar>("/estadoatencion/consultar", ApplyTo.Get);
			Routes.Add<EstadoAtencionCrear>("/estadoatencion/crear", ApplyTo.Post);
			Routes.Add<EstadoAtencionBorrar>("/estadoatencion/borrar", ApplyTo.Post);
			Routes.Add<RecibeRacionConsultar>("/reciberacion/consultar", ApplyTo.Get);
			Routes.Add<RecibeRacionCrear>("/reciberacion/crear", ApplyTo.Post);
			Routes.Add<RecibeRacionBorrar>("/reciberacion/borrar", ApplyTo.Post);
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

	}
}