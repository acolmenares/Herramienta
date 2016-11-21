using System;
using System.Collections.Generic;
using System.Linq;
using Herramienta.Modelos;
using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Metadata;
using Herramienta.Modelos.Objetivos;
using static Herramienta.Modelos.Constantes;

namespace Herramienta.CapaNegocios.Reglas
{
	public abstract class GeneradorNecesidadBasica<TDestino>
		: GeneradorNecesidadBasicaBase<TDestino>
		where TDestino : NecesidadBasica, new()
	{
		Declaracion _declaracion;

		public GeneradorNecesidadBasica(IContenedorMetodos<TDestino> contenedorMetodos) : base(contenedorMetodos)
		{
			_declaracion = new Declaracion();
	
			RegistrarMetodo(f => f.Id, (tr, persona) => ConsultarDeclaracion(tr, persona).Id);
			RegistrarMetodo(f => f.Barrio, (tr, persona) => ConsultarContacto(tr, persona, ContactoBarrioId).Descripcion);
			RegistrarMetodo(f => f.Celular, (tr, persona) => ConsultarContacto(tr, persona, ContactoCelularId).Descripcion);
			RegistrarMetodo(f => f.Declaracion, (tr, persona) => ConsultarDeclaracion(tr, persona).Numero_Declaracion);
			RegistrarMetodo(f => f.Direccion, (tr, declaracion) => ConsultarContacto(tr, declaracion, ContactoDireccionId).Descripcion);
			RegistrarMetodo(f => f.EnLinea, (tr, persona) => ConsultarDescripcionPorId(tr, ConsultarDeclaracion(tr, persona).Id_EnLinea));
			RegistrarMetodo(f => f.FechaAtencion, (tr, persona) => ConsultarFechaAtencion(tr, persona));
			RegistrarMetodo(f => f.FechaDeclaracion, (tr, persona) => ConsultarDeclaracion(tr, persona).Fecha_Declaracion);
			RegistrarMetodo(f => f.FechaDesplazamiento, (tr, persona) => ConsultarDeclaracion(tr, persona).Fecha_Desplazamiento);
			RegistrarMetodo(f => f.FechaRadicacion, (tr, persona) => ConsultarDeclaracion(tr, persona).Fecha_Radicacion);

			RegistrarMetodo(f => f.Fuente, (tr, persona) => ConsultarDescripcionPorId(tr, ConsultarDeclaracion(tr, persona).Id_Fuente));
			RegistrarMetodo(f => f.Grupo, (tr, persona) => ConsultarDescripcionPorId(tr, ConsultarDeclaracion(tr, persona).Id_Grupo));
			RegistrarMetodo(f => f.Horario, (tr, persona) => ConsultarDeclaracion(tr, persona).Horario);
			RegistrarMetodo(f => f.Identificacion, (tr, persona) => persona.Identificacion);
			RegistrarMetodo(f => f.MunicipioAtencion, (tr, persona) => ConsultarDescripcionPorId(tr, ConsultarDeclaracion(tr, persona).Id_lugar_fuente));
			RegistrarMetodo(f => f.PrimerApellido, (tr, persona) => persona.Primer_Apellido);
			RegistrarMetodo(f => f.PrimerNombre, (tr, persona) => persona.Primer_Nombre);
			RegistrarMetodo(f => f.Regional, (tr, persona) => ConsultarDescripcionPorId(tr, ConsultarDeclaracion(tr, persona).Id_Regional));

			RegistrarMetodo(f => f.SegundoApellido, (tr, persona) => persona.Segundo_Apellido);
			RegistrarMetodo(f => f.SegundoNombre, (tr, persona) => persona.Segundo_Nombre);
			RegistrarMetodo(f => f.Telefono, (tr, declaracion) => ConsultarContacto(tr, declaracion, ContactoTelefonoId).Descripcion);
			RegistrarMetodo(f => f.TFE, (tr, persona) => ConsultarTFE(tr, persona));
			RegistrarMetodo(f => f.TFR, (tr, persona) => ConsultarTFR(tr, persona));
			RegistrarMetodo(f => f.TI, (tr, persona) => ConsultarDescripcionPorId(tr, persona.Id_Tipo_Identificacion));

			RegistrarMetodo(f => f.TipoDeclarante, (tr, persona) => ConsultarDescripcionPorId(tr, ConsultarDeclaracion(tr, persona).Tipo_Declaracion));

		}

		DateTime? ConsultarFechaAtencion(TablasRango tr, Personas persona)
		{
			var declaracion = ConsultarDeclaracion(tr, persona);
			return declaracion.Id_Atender.HasValue && declaracion.Id_Atender.Value == SiId ? declaracion.Fecha_Valoracion : null;
		}

		protected string ConsultarDescripcionPorId(TablasRango tr, int? idSubtabla)
		{
			var r = tr.SubTablas.FirstOrDefault(q => q.Id == idSubtabla);
			return (r != default(SubTablas)) ? r.Descripcion : string.Empty;
		}

		protected Declaracion ConsultarDeclaracion(TablasRango tablasRango, Personas persona) 
		{
			(_declaracion.Id != persona.Id_Declaracion)
				.SiCumpleEntonces(() => {
					_declaracion = tablasRango.Declaracion
											 .FirstOrDefault(x => x.Id == persona.Id_Declaracion)
											  ?? new Declaracion();
			});

			return _declaracion;
		}


		protected Personas ConsultarPersonaDeclarante(TablasRango tablasRango, Personas persona)
		{
			return tablasRango.Personas.
							  FirstOrDefault(x => x.Id_Declaracion == persona.Id_Declaracion
												 && x.Tipo == PersonaTipo.Declarante.ToString())
							  ?? new Personas();	
		}


		protected PersonasContactos ConsultarContacto(TablasRango tr, Personas persona,
		                                                   int? idTipoContacto)
		{
			return tr.PersonasContactos
					 .FindAll(q =>
				              q.Id_Persona == persona.Id && q.Id_Tipo_Contacto == idTipoContacto)
					 .OrderBy(q => q.Activo)
					 .OrderByDescending(q => q.Id)
					 .Take(1)
					 .SingleOrDefault() ?? new PersonasContactos();
		}


		protected  int ConsultarTFE(TablasRango tablasRango, Personas persona)
		{
			var declaracion = ConsultarDeclaracion(tablasRango, persona);
			return (declaracion.Menores_Ninas.HasValue ? declaracion.Menores_Ninas.Value : 0) +
				(declaracion.Menores_Ninos.HasValue ? declaracion.Menores_Ninos.Value : 0) +
				(declaracion.Recien_Nacidos.HasValue ? declaracion.Recien_Nacidos.Value : 0) +
				(declaracion.Lactantes.HasValue ? declaracion.Lactantes.Value : 0) +
				(declaracion.Resto_Nucleo.HasValue ? declaracion.Resto_Nucleo.Value : 0);
		}


		protected  int? ConsultarTFR(TablasRango tablasRango, Personas persona)
		{
			var declaracion = ConsultarDeclaracion(tablasRango, persona);

			int? conteo = null;

			(declaracion.Fecha_Valoracion.HasValue)
				.SiCumpleEntonces(() =>
			{
				conteo = ConsultarPersonasDeclaracion(tablasRango, declaracion)
					.Count;
			});

			return conteo;
		}


		protected  List<Personas> ConsultarPersonasDeclaracion(TablasRango tr, Declaracion declaracion)
		{
			return tr.Personas
				     .Where(x => x.Id_Declaracion == declaracion.Id)
					 .ToList();
		}
	}
}

