using System.Collections.Generic;
using Herramienta.Modelos.Extensiones;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Objetivos
{
	public class RecibeRacion:IIndicadorNecesidadBasica
	{
		public class MotivoExclusion
		{
			public int Id { get; set; }
			public string Motivo { get; set; }
			public int Cantidad { get; set; }
		}


		public RecibeRacion()
		{
			MotivosExclusion = new List<MotivoExclusion>();
		}

		public string Periodo { get; set; }
		public string Regional { get; set; }
		public string Municipio { get; set; }
		public string AnioMes { get; set; }

		public string Mes { get { return AnioMes.AnioMesEnMes(); } }

		public int Radicados { get; set; }
		public int DobleDeclaracion { get; set; }
		public int SuperoLimite { get; set; }
		public int NoAsisistioDosProgramaciones { get; set; }
		public int IncluyoPersonaDeOtroNucleo { get; set; }
		public int Extemporaneidad { get; set; }
		public int FueraDeLaCiudad { get; set; }
		public int NoEsDeplazado { get; set; }
		public int Masivo { get; set; }
		public int AtendidoPorOtraFuente { get; set; }
		public int Intraurbano { get; set; }
		public int CultivosIlicitos { get; set; }

		public List<MotivoExclusion> MotivosExclusion { get; set; }
		public int Excluidos { get; set; }
		public int Elegibles { get; set; }

		public int AtendidosPrimeraEntregaEnElMesDeRadicacion { get; set; }
		public int AtendidosPrimeraEntregaEnMesesPosteriores { get; set; }
		public int AtendidosPrimeraEntregaRadicadosEnMesesAnteriores { get; set; }
		public int AtendidosPrimeraEntregaEnPeriodosPosteriores { get; set; }
		public int AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores { get; set; }
		public int TotalAtendidosEnPrimeraEntregaEnElMes { get; set; }
		public int TotalAtendidosEnPrimeraEntregaRadicadosEnElMes { get; set; }
		public int AtendidosEnSegundaEntrega { get; set; }

		public int PendientePorAplicarFiltros { get; set; }
		public int PendientePorProgramar { get; set; }
		public int PendienteNoContactado { get; set; }
		public int PendienteProgramadoProximoMes { get; set; }
		public int PendienteProgramadoQueNoAsistio { get; set; }
		public int TotalPendientesPorAtender { get; set; }

		public int SumaComprobacion
		{
			get
			{
				return Excluidos
					+ AtendidosPrimeraEntregaEnElMesDeRadicacion
					+ AtendidosPrimeraEntregaEnMesesPosteriores
					+ AtendidosPrimeraEntregaEnPeriodosPosteriores
					+ TotalPendientesPorAtender;
			}
		}
		 	

		public double PorcentajeAtendidos
		{
			get
			{
				return Elegibles > 0 ?
					(double)TotalAtendidosEnPrimeraEntregaRadicadosEnElMes / (double)Elegibles :
					0;
			}
		}

		public bool SumaDatosAtendidosEsIgual
		{
			get
			{
				return Elegibles -
					(TotalAtendidosEnPrimeraEntregaRadicadosEnElMes + TotalPendientesPorAtender) == 0;

			} 
		}

		public bool SumaDatosExlcuidosEsIgual
		{
			get
			{
				return Excluidos - (
					  DobleDeclaracion
					+ SuperoLimite
					+ NoAsisistioDosProgramaciones
					+ IncluyoPersonaDeOtroNucleo
					+ Extemporaneidad
					+ FueraDeLaCiudad
					+ NoEsDeplazado
					+ Masivo
					+ AtendidoPorOtraFuente
					+ Intraurbano
					+ CultivosIlicitos
					) == 0;
			}
		}

		public bool SumaComprobacionEsIgual { get { return Radicados - SumaComprobacion == 0; } }
	}
}
