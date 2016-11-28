using System;
using System.Collections.Generic;
using System.Linq;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;
using Herramienta.Modelos.Objetivos;
using static Herramienta.Modelos.Constantes;

namespace Herramienta.CapaNegocios.Reglas
{
	public class FiltroRecibeRacion : FiltroListadosBase<RecibeRacion, EstadoAtencion>
	{
		public FiltroRecibeRacion(IContenedorFiltros<RecibeRacion, EstadoAtencion> contenedorFiltros)
			: base(contenedorFiltros)
		{

			RegistrarFiltroDeConteo(p => p.Radicados, ConsultarRadicados);

			RegistrarFiltroDeConteo(
				p => p.DobleDeclaracion,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					DobleDeclaracionId)
			);

			RegistrarFiltroDeConteo(
				p => p.SuperoLimite,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					new int?[] { SuperoLimiteId, NoUbicadoId })
			);


			RegistrarFiltroDeConteo(
				p => p.NoAsisistioDosProgramaciones,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					NoAsisistioDosProgramacionesId)
			);

			RegistrarFiltroDeConteo(
				p => p.IncluyoPersonaDeOtroNucleo,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					IncluyoPersonaDeOtroNucleoId)
			);

			RegistrarFiltroDeConteo(
				p => p.Extemporaneidad,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					ExtemporaneidadId)
			);

			RegistrarFiltroDeConteo(
				p => p.FueraDeLaCiudad,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					FueraDeLaCiudadId)
			);

			RegistrarFiltroDeConteo(
				p => p.NoEsDeplazado,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					NoEsDeplazadoId)
			);

			RegistrarFiltroDeConteo(
				p => p.Masivo,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					MasivoId)
			);

			RegistrarFiltroDeConteo(
				p => p.AtendidoPorOtraFuente,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					AtendidoPorOtraFuenteId)
			);

			RegistrarFiltroDeConteo(
				p => p.Intraurbano,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					IntraurbanoId)
			);

			RegistrarFiltroDeConteo(
				p => p.CultivosIlicitos,
				(lista, grupo) =>
				ConsultarPorMotivoNoAtencion(
					lista,
					grupo,
					CultivosIlicitosId)
			);


			RegistrarFiltroDeConteo(
				p => p.Excluidos,
				ConsultarExcluidos
			);

			RegistrarFiltroDeConteo(
				p => p.Elegibles,
				ConsultarElegibles
			);

			RegistrarFiltroDeConteo(
				p => p.AtendidosPrimeraEntregaEnElMesDeRadicacion,
				ConsultarAtendidosPrimeraEntregaEnElMesDeRadicacion
			);

			RegistrarFiltroDeConteo(
				p => p.AtendidosPrimeraEntregaEnMesesPosteriores,
				ConsultarAtendidosPrimeraEntregaEnMesesPosteriores
			);

			RegistrarFiltroDeConteo(
				p => p.AtendidosPrimeraEntregaRadicadosEnMesesAnteriores,
				ConsultarAtendidosPrimeraEntregaRadicadosEnMesesAnteriores
			);

			RegistrarFiltroDeConteo(
				p => p.AtendidosPrimeraEntregaEnPeriodosPosteriores,
				ConsultarAtendidosPrimeraEntregaEnPeriodosPosteriores

			);

			RegistrarFiltroDeConteo(
				p => p.AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores,
				ConsultarAtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores
			);

			RegistrarFiltroDeConteo(
				p => p.TotalAtendidosEnPrimeraEntregaEnElMes,
				ConsultarTotalAtendidosEnPrimeraEntregaEnElMes
			);

			RegistrarFiltroDeConteo(
				p => p.TotalAtendidosEnPrimeraEntregaRadicadosEnElMes,
				ConsultarTotalAtendidosEnPrimeraEntregaRadicadosEnElMes
			);

			RegistrarFiltroDeConteo(
				p => p.AtendidosEnSegundaEntrega,
				ConsultarAtendidosEnSegundaEntrega
			);

			RegistrarFiltroDeConteo(
				p => p.PendientePorAplicarFiltros,
				ConsultarPendientePorAplicarFiltros
			);

			RegistrarFiltroDeConteo(
				p => p.PendientePorProgramar,
				ConsultarPendientePorProgramar
			);

			RegistrarFiltroDeConteo(
				p => p.PendienteNoContactado,
				ConsultarPendienteNoContactado
			);

			RegistrarFiltroDeConteo(
				p => p.PendienteProgramadoProximoMes,
				ConsultarPendienteProgramadoProximoMes
			);

			RegistrarFiltroDeConteo(
				p => p.PendienteProgramadoQueNoAsistio,
				ConsultarPendienteProgramadoQueNoAsistio
			);

			RegistrarFiltroDeConteo(
				p => p.TotalPendientesPorAtender,
				ConsultarTotalPendientesPorAtender
			);
		}


		public override List<RecibeRacion> AplicarFiltros(List<EstadoAtencion> lista)
		{
			var soloDesplazados = lista.Where(q => q.TipoDeclarante == DESPLAZADO).ToList();

			var listaRecibeRacion = GenerarListaRecibeAtencionConDatosBasicos(soloDesplazados);


			listaRecibeRacion.ForEach(recibeRacion =>
			{
				ActualizarValoresDeConteo(recibeRacion, soloDesplazados);
				recibeRacion.MotivosExclusion = ConsultarMotivosExclusion(soloDesplazados, recibeRacion);
			});

			return listaRecibeRacion;
		}



		List<RecibeRacion> GenerarListaRecibeAtencionConDatosBasicos(List<EstadoAtencion> listaEstadoAtencion)
		{
			return listaEstadoAtencion
				.Where(q => q.TipoDeclarante == DESPLAZADO).ToList()
				.GroupBy(x => new { x.PeriodoRadicacion, x.Regional, x.MunicipioAtencion, AnioMes = x.AnioMesRadicacion })
				.Select(r => new RecibeRacion
				{
					Periodo = r.Key.PeriodoRadicacion,
					Regional = r.Key.Regional,
					Municipio = r.Key.MunicipioAtencion,
					AnioMes = r.Key.AnioMes,
				}).ToList();
		}


		List<EstadoAtencion> ConsultarTotalPendientesPorAtender(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			var r = new List<EstadoAtencion>();
			var l1 = ConsultarPendientePorAplicarFiltros(lista, grupo);
			var l2 = ConsultarPendientePorProgramar(lista, grupo);
			var l3 = ConsultarPendienteNoContactado(lista, grupo);
			var l4 = ConsultarPendienteProgramadoProximoMes(lista, grupo);
			var l5 = ConsultarPendienteProgramadoQueNoAsistio(lista, grupo);

			r.AddRange(l1);
			r.AddRange(l2);
			r.AddRange(l3);
			r.AddRange(l4);
			r.AddRange(l5);
			return r;
		}


		List<EstadoAtencion> ConsultarPendienteProgramadoQueNoAsistio(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(x => NoAtendidoQuePermaneceElegible(x)
						   && x.Contactado == SI
						   && x.Programado == SI
						   && EsProgramadoQueNoAsistio(x)
                           && !x.AnioMesProgramado.EstaVacia()
					       && x.AnioMesProgramado == (grupo.AnioMes?? x.AnioMesProgramado)
						  )
					.ToList();
		}


		List<EstadoAtencion> ConsultarPendienteProgramadoProximoMes(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(x => NoAtendidoQuePermaneceElegible(x)
						   && x.Contactado == SI
						   && x.Programado == SI
					       //&& !EsProgramadoQueNoAsistio(x)
                           && !x.AnioMesProgramado.EstaVacia()
						   && x.AnioMesProgramado != grupo.AnioMes
						  )
					.ToList();
		}


		List<EstadoAtencion> ConsultarPendienteNoContactado(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return
				ConsultarRadicados(lista,grupo)
					.Where(x => NoAtendidoQuePermaneceElegible(x)
						   && x.Contactado != SI  )
					.ToList();
		}

		List<EstadoAtencion> ConsultarPendientePorProgramar(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(x => NoAtendidoQuePermaneceElegible(x)
						   && x.Contactado == SI
						   && x.Programado != SI)
					.ToList();
		}

		List<EstadoAtencion> ConsultarPendientePorAplicarFiltros(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return ConsultarRadicados(lista, grupo)
				.Where(q => !Atendido(q) && q.Elegibilidad.EstaVacia())
				.ToList();
		}

		List<EstadoAtencion> ConsultarAtendidosEnSegundaEntrega(List<EstadoAtencion> lista,
		                                                        RecibeRacion grupo)
		{
			return
				lista.Where(q => q.Regional == (grupo.Regional?? q.Regional)
							&& q.MunicipioAtencion == (grupo.Municipio?? q.MunicipioAtencion)
							&& Atendido(q)
							&& q.AsistioSegundaEntrega == SI
							&& q.AnioMesSegundaEntrega == ( grupo.AnioMes??q.AnioMesSegundaEntrega))
					 .ToList();
		}

		List<EstadoAtencion> ConsultarTotalAtendidosEnPrimeraEntregaRadicadosEnElMes(List<EstadoAtencion> lista,
		                                                                             RecibeRacion grupo)
		{
			var r = new List<EstadoAtencion>();
			var l1 = ConsultarAtendidosPrimeraEntregaEnElMesDeRadicacion(lista, grupo);
			var l2 = ConsultarAtendidosPrimeraEntregaEnMesesPosteriores(lista, grupo);
			var l3 = ConsultarAtendidosPrimeraEntregaEnPeriodosPosteriores(lista, grupo);
			r.AddRange(l1);
			r.AddRange(l2);
			r.AddRange(l3);
			return r;

		}

		List<EstadoAtencion> ConsultarTotalAtendidosEnPrimeraEntregaEnElMes(List<EstadoAtencion> lista,
		                                                                    RecibeRacion grupo)
		{
			var r = new List<EstadoAtencion>();
			var l1 = ConsultarAtendidosPrimeraEntregaEnElMesDeRadicacion(lista, grupo);
			var l2 = ConsultarAtendidosPrimeraEntregaRadicadosEnMesesAnteriores(lista, grupo);
			var l3 = ConsultarAtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores(lista, grupo);
			r.AddRange(l1);
			r.AddRange(l2);
			r.AddRange(l3);
			return r;

		}

		List<EstadoAtencion> ConsultarAtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores(List<EstadoAtencion> lista,
		                                                                                   RecibeRacion grupo)
		{
            return AgruparPorAnioMesAtencion(lista, grupo, (l, g) =>
            {
                return
                lista
                    .Where(q => Atendido(q)
                           && q.Regional == (g.Regional ?? q.Regional)
                           && q.MunicipioAtencion == g.Municipio
                           && q.AnioMesAtencion ==  g.AnioMes
                           && q.PeriodoRadicacion != g.Periodo)
                    .ToList();
            });
			
		}

		List<EstadoAtencion> ConsultarAtendidosPrimeraEntregaEnPeriodosPosteriores(List<EstadoAtencion> lista, 
		                                                                           RecibeRacion grupo)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(q => Atendido(q)
						   && q.PeriodoAtencion != q.PeriodoRadicacion)
					.ToList();
			
		}


        List<EstadoAtencion> ConsultarAtendidosPrimeraEntregaRadicadosEnMesesAnteriores(List<EstadoAtencion> lista,
                                                                                        RecibeRacion grupo)
        {
            return AgruparPorAnioMesAtencion(lista, grupo, (l, g) =>
            {
                return l
                .Where(q => Atendido(q)
                   && q.Regional == (g.Regional ?? q.Regional)
                   && q.MunicipioAtencion == g.Municipio
                   && q.AnioMesAtencion ==  g.AnioMes
                   && q.PeriodoRadicacion == g.Periodo
                   && q.AnioMesRadicacion != g.AnioMes
                  )
            .ToList();
            });

        }

        private List<EstadoAtencion> AgruparPorAnioMesAtencion(List<EstadoAtencion> lista, RecibeRacion grupo, Func<List<EstadoAtencion>, RecibeRacion, List<EstadoAtencion>> func)
        {
            var res = new List<EstadoAtencion>();

            var grupos = lista.Where(q=>Atendido(q) && q.AnioMesAtencion==(grupo.AnioMes??q.AnioMesAtencion) ).GroupBy(x => x.AnioMesAtencion).ToList();
            foreach( var g in grupos)
            {
                var grupoDelAnioMes = new RecibeRacion
                {
                    Regional = grupo.Regional,
                    Municipio = grupo.Municipio,
                    AnioMes = g.Key,
                    Periodo = g.Key.AnioMesEnPeriodo(),

                };
                res.AddRange(func(g.ToList(), grupoDelAnioMes));
            }
            
            return res;
        }

        List<EstadoAtencion> ConsultarAtendidosPrimeraEntregaEnMesesPosteriores(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(q => Atendido(q)
						   && q.AnioMesAtencion != q.AnioMesRadicacion
						   && q.PeriodoAtencion == q.PeriodoRadicacion)
					.ToList();
		}

		List<EstadoAtencion> ConsultarAtendidosPrimeraEntregaEnElMesDeRadicacion(
			List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return ConsultarRadicados(lista, grupo)
				.Where(q => Atendido(q) && q.AnioMesAtencion == q.AnioMesRadicacion)
				.ToList();
		}


		List<EstadoAtencion> ConsultarPorMotivoNoAtencion(List<EstadoAtencion> lista, RecibeRacion grupo, int motivoNoAtencionId)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(q => !Atendido(q)
						   && q.MotivoNoAtencionId.HasValue && q.MotivoNoAtencionId.Value == motivoNoAtencionId)
					.ToList();
		}



		List<EstadoAtencion> ConsultarPorMotivoNoAtencion(List<EstadoAtencion> lista, RecibeRacion grupo,
														  IList<int?> listNotivoNoAtencionId)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(q => !Atendido(q)
						   && q.MotivoNoAtencionId.HasValue && listNotivoNoAtencionId.Contains(q.MotivoNoAtencionId))
					.ToList();

		}


		List<EstadoAtencion> ConsultarRadicados(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return (grupo != null)
				? lista.Where(q =>
							 q.PeriodoRadicacion == (grupo.Periodo ?? q.PeriodoRadicacion)
							 && q.Regional == (grupo.Regional ?? q.Regional)
							 && q.MunicipioAtencion == (grupo.Municipio ?? q.MunicipioAtencion)
							 && q.AnioMesRadicacion == (grupo.AnioMes ?? q.AnioMesRadicacion))
					   .ToList()
						   : lista;

		}


		List<EstadoAtencion> ConsultarExcluidos(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(f => !Atendido(f) && NoElegible(f)  ).ToList();
		}

		List<EstadoAtencion> ConsultarElegibles(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return
				ConsultarRadicados(lista, grupo)
					.Where(f => Atendido(f) || !NoElegible(f)).ToList(); //todo Revisar sinfiltro
		}



		private bool Atendido(EstadoAtencion dato)
		{
			return !dato.MotivoNoAtencionId.HasValue && dato.FechaAtencion.HasValue;
		}


		// Para corregir las primeras consultas de Junio/Julio  2016 que pueden traer mal el valor de Elegibiidad
		// cuando quedo Si 
		// pero tienen un motivo de atención diferente a Programado que no asistio.
		// en ese caso debería venir No. 
		private bool NoAtendidoQuePermaneceElegible(EstadoAtencion dato)
		{
			return !Atendido(dato)
				&& (!dato.MotivoNoAtencionId.HasValue || (EsProgramadoQueNoAsistio(dato))
					&& dato.Elegibilidad == SI);
			//return !Atendido(dato) && EsProgramadoQueNoAsistio(dato) && dato.Elegibilidad == SI;
			//return !EsProgramadoQueNoAsistio(dato) ? false : dato.Elegibilidad == SI;
			//return EsProgramadoQueNoAsistio(dato) || dato.Elegibilidad == SI;
			//return NoEsProgramadoQueNoAsistio(dato) ? false : dato.Elegibilidad == SI;
			//return !NoEsProgramadoQueNoAsistio(dato) && dato.Elegibilidad == SI;
		}


		private bool EsProgramadoQueNoAsistio(EstadoAtencion dato)
		{
			return dato.MotivoNoAtencionId.HasValue && dato.MotivoNoAtencionId == ProgramadoQueNoAsistioId;
			//return dato.MotivoNoAtencionId.HasValue && dato.MotivoNoAtencionId != ProgramadoQueNoAsistioId;
		}


		private bool NoElegible(EstadoAtencion dato)
		{
			return dato.Elegibilidad == NO;
		}



		private List<RecibeRacion.MotivoExclusion> ConsultarMotivosExclusion(List<EstadoAtencion> lista, RecibeRacion grupo)
		{
			return
				ConsultarExcluidos(lista, grupo)
					.GroupBy(f => f.MotivoNoAtencion)
					.Select(x => new RecibeRacion.MotivoExclusion
					{
						Id = x.First().MotivoNoAtencionId.Value,
						Motivo = x.Key,
						Cantidad = x.Count()
					}).ToList();
		}

	}
}
