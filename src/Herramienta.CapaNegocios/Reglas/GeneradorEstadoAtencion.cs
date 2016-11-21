using Herramienta.Modelos.Entidades;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Metadata;
using Herramienta.Modelos.Objetivos;
using Herramienta.Modelos.Extensiones;
using System.Linq;
using static Herramienta.Modelos.Constantes;

namespace Herramienta.CapaNegocios.Reglas
{
	public class GeneradorEstadoAtencion : GeneradorNecesidadBasica<EstadoAtencion>,
	IGeneradorNecesidadBasica<EstadoAtencion>
	{
		DeclaracionEstados _programadoPrimeraEntrega;
		DeclaracionEstados _programadoSegundaEntrega;
		DeclaracionEstados _contactado;
		DeclaracionEstados _elegible;

		public GeneradorEstadoAtencion(IContenedorMetodos<EstadoAtencion> contenedorMetodos)
			:base(contenedorMetodos)
		{
			_programadoPrimeraEntrega = new DeclaracionEstados();
			_programadoSegundaEntrega = new DeclaracionEstados();

			_contactado = new DeclaracionEstados();
			_elegible = new DeclaracionEstados();

			RegistrarMetodo(f => f.Elegibilidad, (tr, persona) => ConsultarComoEstado(tr, ConsultarElegibilidad(tr, persona)));
			RegistrarMetodo(f => f.FechaElegibilidad, (tr, persona) => ConsultarElegibilidad(tr, persona).Fecha);

			RegistrarMetodo(f => f.Contactado, (tr, persona) => ConsultarComoEstado(tr, ConsultarContactado(tr, persona)));
			RegistrarMetodo(f => f.FechaContactado, (tr, persona) => ConsultarContactado(tr, persona).Fecha);

			RegistrarMetodo(f => f.Programado, (tr, persona) => ConsultarComoEstado(tr, ConsultarProgramadoPrimeraEntrega(tr, persona)));
			RegistrarMetodo(f => f.FechaProgramado, (tr, persona) => ConsultarProgramadoPrimeraEntrega(tr, persona).Fecha);

			RegistrarMetodo(f => f.Reprogramado, (tr, persona) => ConsultarComoEstado(tr, ConsultarReprogramado(tr, persona)));
			RegistrarMetodo(f => f.FechaReprogramado, (tr, persona) => ConsultarReprogramado(tr, persona).Fecha);

			RegistrarMetodo(f => f.Atendido, (tr, persona) => ConsultarAtendido(tr, persona));

			RegistrarMetodo(f => f.MotivoNoAtencion, (tr, persona) => ConsultarDescripcionPorId(tr, ConsultarDeclaracion(tr, persona).Id_Motivo_Noatender));
			RegistrarMetodo(f => f.MotivoNoAtencionId, (tr, persona) => ConsultarDeclaracion(tr, persona).Id_Motivo_Noatender);

			RegistrarMetodo(f => f.TipoReprogramacion, (tr, persona) => ConsultarTipoReprogramacion(tr, persona));

			RegistrarMetodo(f => f.FechaSegundaEntrega, (tr, persona) => ConsultarProgramadoSegundaEntrega(tr, persona).Fecha);

			RegistrarMetodo(f => f.AsistioSegundaEntrega, (tr, persona) => ConsultarDescripcionPorId(tr, ConsultarProgramadoSegundaEntrega(tr, persona).Id_Asistio));

		}


		string ConsultarAtendido(TablasRango tr, Personas persona)
		{
			var declaracion = ConsultarDeclaracion(tr, persona);
			var atendido = declaracion.Id_Motivo_Noatender.HasValue && declaracion.Id_Motivo_Noatender.Value != ProgramadoQueNoAsistioId ? NoId : declaracion.Id_Atender;
			return ConsultarDescripcionPorId(tr, atendido);
		}


		DeclaracionEstados ConsultarElegibilidad(TablasRango tr, Personas persona)
		{
			(_elegible.Id_Declaracion != persona.Id_Declaracion)
				.SiCumpleEntonces(() =>
				{
					_elegible = ConsultarDeclaracionEstado(tr, persona, ElegibleId);
					var declaracion = ConsultarDeclaracion(tr, persona);
					(declaracion.Id_Motivo_Noatender.HasValue && declaracion.Id_Motivo_Noatender != ProgramadoQueNoAsistioId)
					.SiCumpleEntonces(() =>
					{
						_elegible.Id_Como_Estado = NoId;
					});

				});

			return _elegible;
		}


		DeclaracionEstados ConsultarContactado(TablasRango tr, Personas persona)
		{
			(_contactado.Id_Declaracion != persona.Id_Declaracion)
				.SiCumpleEntonces(() =>
				{
					_contactado = ConsultarDeclaracionEstado(tr, persona, ContactadoId);
				});
			return _contactado;
		}


		DeclaracionEstados ConsultarReprogramado(TablasRango tr, Personas persona)
		{
			var pe = ConsultarProgramadoPrimeraEntrega(tr, persona);
			var se = ConsultarProgramadoSegundaEntrega(tr, persona);
			return se.Id_Tipo_Estado == ReprogramadoId ? se : pe.Id_Tipo_Estado == ReprogramadoId ? pe : new DeclaracionEstados();
		}


		string ConsultarTipoReprogramacion(TablasRango tr, Personas persona)
		{
			var estado = ConsultarReprogramado(tr, persona);
			var programa = tr.Programacion
							 .FirstOrDefault(x => x.Id == estado.Id_Programa) ?? new Programacion();
			return ConsultarDescripcionPorId(tr, programa.Id_TipoEntrega);
		}


		DeclaracionEstados ConsultarProgramadoPrimeraEntrega(TablasRango tr, Personas persona)
		{
			(_programadoPrimeraEntrega.Id_Declaracion != persona.Id_Declaracion)
				.SiCumpleEntonces(() =>
				{
					_programadoPrimeraEntrega = ConsultarProgramado(tr, persona, PrimeraEntregaId);
				});

			return _programadoPrimeraEntrega;
		}


		DeclaracionEstados ConsultarProgramadoSegundaEntrega(TablasRango tr, Personas persona)
		{
			(_programadoSegundaEntrega.Id_Declaracion != persona.Id_Declaracion)
				.SiCumpleEntonces(() =>
				{
					_programadoSegundaEntrega = ConsultarProgramado(tr, persona, SegundaEntregaId);
				});

			return _programadoSegundaEntrega;

		}



		DeclaracionEstados ConsultarProgramado(TablasRango tablasRango, Personas persona, int tipoEntregaId)
		{
			return tablasRango.DeclaracionEstados
							  .Where(q => q.Id_Declaracion == persona.Id_Declaracion
									 && (q.Id_Tipo_Estado == ProgramadoId || q.Id_Tipo_Estado == ReprogramadoId)
									 && q.Id_Como_Estado == SiId)
							  .Join(tablasRango.Programacion
									.Where(x => x.Id_TipoEntrega == tipoEntregaId),
									a => a.Id_Programa,
									b => b.Id, (a, b) =>
									new DeclaracionEstados
									{
										Id = a.Id,
										Fecha = b.Fecha,
										Id_Declaracion = a.Id_Declaracion,
										Id_Tipo_Estado = a.Id_Tipo_Estado,
										Id_Como_Estado = a.Id_Como_Estado,
										Id_Programa = a.Id_Programa,
										Id_Asistio = a.Id_Asistio
									})
							  .OrderByDescending(f => f.Fecha)
							  .ThenByDescending(f => f.Id)
							  .Take(1)
							  .SingleOrDefault()
							  ?? new DeclaracionEstados();
		}


		DeclaracionEstados ConsultarDeclaracionEstado(TablasRango tr, Personas persona, int tipoEstadoId)
		{
			return tr.DeclaracionEstados
					 .FindAll(q => q.Id_Declaracion == persona.Id_Declaracion && q.Id_Tipo_Estado == tipoEstadoId)
					 .OrderByDescending(q => q.Fecha)
					 .ThenByDescending(q => q.Id)
					 .Take(1)
					 .SingleOrDefault()
					 ?? new DeclaracionEstados();
		}

		string ConsultarComoEstado(TablasRango tr, DeclaracionEstados estado)
		{
			return ConsultarDescripcionPorId(tr, estado.Id_Como_Estado);
		}

	}
}
