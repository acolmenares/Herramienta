using System;
using System.Collections.Generic;
using System.IO;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Metadata;
using ServiceStack;
using ServiceStack.Text;
using Herramienta.Modelos.Extensiones;
using System.Linq;

namespace Herramienta.CapaDatos
{
	public class RepositorioHerramienta : IRepositorioHerramienta
	{
		readonly DirectoryInfo directorioRaiz;
		readonly DirectoryInfo metaDir;
		readonly ITransformoFechas transformoFechas;

		public RepositorioHerramienta(ITransformoFechas transformoFechas, string directorioRaiz = "Herramienta")
		{
			this.directorioRaiz = new DirectoryInfo(PathUtils.CombinePaths("~", "App_Data", directorioRaiz).MapHostAbsolutePath());
			if (!this.directorioRaiz.Exists) this.directorioRaiz.Create();
			metaDir = this.directorioRaiz.CreateSubdirectory("Meta");
			this.transformoFechas = transformoFechas;
		}

		public bool ExisteRango(ITengoFechaRadicacionDesdeHasta query)
		{
			var rango = transformoFechas.ConvertirEnRango(query);
			var _dirrango = new DirectoryInfo(Path.Combine(directorioRaiz.FullName, rango));
			return (_dirrango.Exists);
		}


		public List<Rango> ConsultarRango()
		{
			var fn = NombreEnMetDir<Rango>();
			return File.Exists(fn) ? ReadListFromFile<Rango>(fn) : new List<Rango>();
		}

		public List<Rango> ConsultarRango(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			return ConsultarRango();
		}


		public List<T> ConsultarDatoObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : INecesidadBasica
		{
			return ConsultarImp<T>(rangoFechas);
		}

		public List<T> ConsultarTablaRango<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) where T : IEntidad
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			return ObtenerTablaRango<T>(rango);
		}


		public TablasRango ConsultarTablasRango(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			var tablas = new TablasRango();
			tablas.Declaracion = ObtenerTablaRango<Declaracion>(rango);
			tablas.DeclaracionEstados = ObtenerTablaRango<DeclaracionEstados>(rango);
			tablas.Personas = ObtenerTablaRango<Personas>(rango);
			tablas.PersonasContactos = ObtenerTablaRango<PersonasContactos>(rango);
			tablas.PersonasEducacion = ObtenerTablaRango<PersonasEducacion>(rango);
			tablas.PersonasRegimenSalud = ObtenerTablaRango<PersonasRegimenSalud>(rango);
			tablas.Programacion = ObtenerTablaRango<Programacion>(rango);
			tablas.SubTablas = ObtenerTablaRango<SubTablas>(rango);
			return tablas;
		}


		public bool ExisteDatoObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) where T : INecesidadBasica
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			return ExisteJson<T>(rango);
		}


		private List<T> ObtenerTablaRango<T>(string rango)
		{
			var archivoJson = NombreCompletoJson<T>(rango);
			return File.Exists(archivoJson) ? ReadListFromFile<T>(archivoJson) : new List<T>();
		}


		protected string NombreCompletoJson<T>(string rango)
		{
			var type = typeof(T);
			return NombreCompletoJson(rango, type);
			}

		string NombreCompletoJson(string rango, Type type)
		{
			var name = type.Name;
			return Path.Combine(directorioRaiz.FullName, rango, name + ".json");
		}


		protected string NombreEnMetDir<T>()
		{
			return Path.Combine(metaDir.FullName, typeof(T).Name + ".json");
		}


		protected bool ExisteJson<T>(string rango)
		{
			return File.Exists(NombreCompletoJson<T>(rango));
		}

		private bool ExisteJson(string rango, Type tipo)
		{
			return File.Exists(NombreCompletoJson(rango,tipo));
		}


		protected static T ReadFromFile<T>(string fileName)
		{
			var r = default(T);
			using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				r = JsonSerializer.DeserializeFromStream<T>(fileStream);
			}
			return r;
		}

		protected static List<T> ReadListFromFile<T>(string fileName)
		{
			var r = new List<T>();
			using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				r = JsonSerializer.DeserializeFromStream<List<T>>(fileStream);
			}
			return r;
		}


		protected static void SaveToFile<T>(T data, string fileName, bool sobreescribir = false, bool sololectura = false)
		{
			if (sobreescribir || !File.Exists(fileName))
			{
				using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
				{
					JsonSerializer.SerializeToStream(data, fileStream);
				}
				if (sololectura)
				{
					File.SetAttributes(fileName, FileAttributes.ReadOnly);
				}
			}
		}


		protected  void DeleteFile<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			var archivoJson = NombreCompletoJson<T>(rango);
			File.Exists(archivoJson).SiCumpleEntonces(() =>
			{
				File.Delete(archivoJson);
			});
		}

		protected DirectoryInfo ObtenerDirectorioLanzarErrorSiNoExiste(string rango)
		{
			var _dirrango = new DirectoryInfo(Path.Combine(directorioRaiz.FullName, rango));
			if (!_dirrango.Exists)
			{
				throw new Exception(string.Format("Rango '{0}' NO ya existe. verifique las fechas", rango));
			}
			return _dirrango;
		}


		protected DirectoryInfo ObtenerDirectorioLanzarErrorSiExiste(string rango)
		{
			var _dirrango = new DirectoryInfo(Path.Combine(directorioRaiz.FullName, rango));
			if (_dirrango.Exists)
			{
				throw new Exception(string.Format("Rango '{0}' ya existe. Debe ser borrado primero para volver a crearlo", rango));
			}
			return _dirrango;
		}

		public void LanzarCuandoExiste<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : INecesidadBasica
		{
			LanzarCuandoExiste(rangoFechas, typeof(T));
		}


		public void LanzarCuandoNoExiste<T>(ITengoFechaRadicacionDesdeHasta rangoFechas)
			where T : INecesidadBasica
		{
			LanzarCuandoNoExiste(rangoFechas, typeof(T));
		}


		public void CrearRango(ITengoFechaRadicacionDesdeHasta rangoFechas, TablasRango tablasRango)
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			var _dirrango = ObtenerDirectorioLanzarErrorSiExiste(rango);

			_dirrango.Create();

			CrearRangoImp(rango, tablasRango.SubTablas, sololectura: true);
			CrearRangoImp(rango, tablasRango.Programacion, sololectura: true);
			CrearRangoImp(rango, tablasRango.Declaracion, sololectura: true);
			CrearRangoImp(rango, tablasRango.Personas, sololectura: true);
			CrearRangoImp(rango, tablasRango.PersonasContactos, sololectura: true);
			CrearRangoImp(rango, tablasRango.PersonasEducacion, sololectura: true);
			CrearRangoImp(rango, tablasRango.PersonasRegimenSalud, sololectura: true);
			CrearRangoImp(rango, tablasRango.DeclaracionBienes, sololectura: true);
			CrearRangoImp(rango, tablasRango.DeclaracionCausasDesplazamiento, sololectura: true);
			CrearRangoImp(rango, tablasRango.DeclaracionDaniosFamilia, sololectura: true);
			CrearRangoImp(rango, tablasRango.DeclaracionEstados, sololectura: true);
			CrearRangoImp(rango, tablasRango.DeclaracionFuentesIngreso, sololectura: true);
			CrearRangoImp(rango, tablasRango.DeclaracionObtencionAgua, sololectura: true);
			CrearRangoImp(rango, tablasRango.DeclaracionPersonasAyuda, sololectura: true);
			CrearRangoImp(rango, tablasRango.DeclaracionPsicosocial, sololectura: true);

			CrearMeta(rango, tablasRango, rangoFechas);
		}


		public void BorrarRango(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			var _dirrango = ObtenerDirectorioLanzarErrorSiNoExiste(rango);
			_dirrango.Delete(true);

			var rangos = ConsultarRango();
			RemoverRangoDeLaLista(rango, rangos);
			GuardarRangos(rangos);
		}

		public void CrearDatoObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas, List<T> datoObjetivoNecesidadesBasicas) where T : INecesidadBasica
		{
			LanzarCuandoExiste<T>(rangoFechas);
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			ObtenerDirectorioLanzarErrorSiNoExiste(rango);
			var archivoJson = NombreCompletoJson<T>(rango);
			SaveToFile(datoObjetivoNecesidadesBasicas, archivoJson);
		}

		public void BorrarDatoObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) where T : INecesidadBasica
		{
			DeleteFile<T>(rangoFechas);
		}

		static void RemoverRangoDeLaLista(string rango, List<Rango> rangos)
		{
			var i = rangos.FindIndex(q => q.Nombre == rango);
			if (i >= 0)
			{
				rangos.RemoveAt(i);
			}
		}

		private void GuardarRangos(List<Rango> rangos)
		{
			var fn = NombreEnMetDir<Rango>();
			SaveToFile(rangos.OrderByDescending(q => q.Nombre).ToList(), fn, sobreescribir: true);
		}

		private void CrearRangoImp<T>(string rango, List<T> data, bool sololectura = false)
		{
			var fn = NombreCompletoJson<T>(rango);
			SaveToFile(data, fn, sololectura: sololectura);
		}



		private void CrearMeta(string rango, TablasRango tablas, ITengoFechaRadicacionDesdeHasta query)
		{
			var regionales = tablas.Declaracion.GroupBy(r => r.Id_Regional).Select(reg => new RegionalObjetivo
			{
				Nombre = tablas.SubTablas.Find(st => st.Id == reg.Key).Descripcion,
				Municipios = reg.GroupBy(mn => mn.Id_lugar_fuente).Select(municipio => new MunicipioObjectivo
				{
					Nombre = tablas.SubTablas.Find(st => st.Id == municipio.Key).Descripcion,
					Regional = tablas.SubTablas.Find(st => st.Id == reg.Key).Descripcion,
					AniosMesesRadicacion = municipio.GroupBy(per => per.Fecha_Radicacion.EnAnioMes()).Select(periodo => new AnioMesObjetivo
					{
						Nombre = periodo.Key,
						Municipio = tablas.SubTablas.Find(st => st.Id == municipio.Key).Descripcion,
						Regional = tablas.SubTablas.Find(st => st.Id == reg.Key).Descripcion,
					}).OrderBy(x => x.Nombre).ToList(),
					AniosMesesAtencion = municipio.Where(q => q.Fecha_Valoracion.HasValue)
												  .GroupBy(per => per.Fecha_Valoracion.EnAnioMes())
					.Select(periodo => new AnioMesObjetivo
					{
						Nombre = periodo.Key,
						Municipio = tablas.SubTablas.Find(st => st.Id == municipio.Key).Descripcion,
						Regional = tablas.SubTablas.Find(st => st.Id == reg.Key).Descripcion,
					}).OrderBy(x => x.Nombre).ToList()
				}).ToList()
			}).ToList();

			var municipios = new List<string>();
			regionales.ForEach(reg =>
			{
				municipios.AddRange(reg.Municipios.GroupBy(g => g.Nombre).Select(q => q.Key));
			});

			var ri = new Rango
			{
				Regionales = regionales,
				Municipios = municipios,
				Nombre = rango,
				Periodo = transformoFechas.ConvertirRangoEnPeriodo(rango),
				AnioMes = transformoFechas.ConvertirRangoEnAnioMes(rango),
				FechaRadicacionDesde = query.FechaRadicacionDesde,
				FechaRadicacionHasta = query.FechaRadicacionHasta
			};

			var rangos = ConsultarRango();
			RemoverRangoDeLaLista(rango, rangos);
			rangos.Add(ri);
			GuardarRangos(rangos);
		}

		public void LanzarExcepcionCuandoNoExisteRango(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			ObtenerDirectorioLanzarErrorSiNoExiste(rango);
		}

		public void LanzarExcepcionCuandoExisteRango(ITengoFechaRadicacionDesdeHasta rangoFechas)
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			ObtenerDirectorioLanzarErrorSiExiste(rango);
		}

		public void CrearIndicadorObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas, List<T> indicadorObjetivo) where T : IIndicadorNecesidadBasica
		{
			LanzarCuandoExiste(rangoFechas, typeof(T));
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			ObtenerDirectorioLanzarErrorSiNoExiste(rango);
			var archivoJson = NombreCompletoJson<T>(rango);
			SaveToFile(indicadorObjetivo, archivoJson);
		}

		public void BorrarIndicadorObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) where T : IIndicadorNecesidadBasica
		{
			LanzarCuandoNoExiste(rangoFechas, typeof(T));
			DeleteFile<T>(rangoFechas);
		}

		public List<T> ConsultarIndicadorObjetivoNecesidadesBasicas<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) where T : IIndicadorNecesidadBasica
		{
			return ConsultarImp<T>(rangoFechas);
		}

		void LanzarCuandoExiste(ITengoFechaRadicacionDesdeHasta rangoFechas, Type tipo) 
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			if (ExisteJson(rango,tipo))
			{
				throw new Exception(string.Format("Elemento {0} ya existe en el Rango {1}", tipo.Name, rango));
			}
		}

		void LanzarCuandoNoExiste(ITengoFechaRadicacionDesdeHasta rangoFechas, Type type)		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			if (!ExisteJson(rango, type))
			{
				throw new Exception(string.Format("Elemento {0} No  existe en el Rango {1}", type.Name, rango));
			}
		}

		List<T> ConsultarImp<T>(ITengoFechaRadicacionDesdeHasta rangoFechas) 
		{
			var rango = transformoFechas.ConvertirEnRango(rangoFechas);
			var archivoJson = NombreCompletoJson<T>(rango);
			return File.Exists(archivoJson) ? ReadListFromFile<T>(archivoJson) : new List<T>();
		}
	}
}

