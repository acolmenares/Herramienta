using System;
using System.Collections.Generic;
using Herramienta.Modelos.Entidades;

namespace Herramienta.Modelos.Metadata
{
	public class TablasRango
    {
		public TablasRango()
		{

			Declaracion = new List<Declaracion>();
			Personas = new List<Personas>();
			Programacion = new List<Programacion>();
			SubTablas = new List<SubTablas>();
			DeclaracionEstados = new List<DeclaracionEstados>();
			PersonasContactos = new List<PersonasContactos>();
			PersonasEducacion = new List<PersonasEducacion>();
			PersonasRegimenSalud = new List<PersonasRegimenSalud>();

			DeclaracionBienes = new List<DeclaracionBienes>();
			DeclaracionCausasDesplazamiento = new List<DeclaracionCausasDesplazamiento>();
			DeclaracionDaniosFamilia = new List<DeclaracionDaniosFamilia>();
			DeclaracionObtencionAgua = new List<DeclaracionObtencionAgua>();
			DeclaracionPersonasAyuda = new List<DeclaracionPersonasAyuda>();
			DeclaracionPsicosocial = new List<DeclaracionPsicosocial>();
		
		}

		public List<Programacion> Programacion { get; set; }
		public List<SubTablas> SubTablas { get; set; }

		public List<Declaracion> Declaracion { get; set; }
        public List<Personas> Personas { get; set; }
                
		public List<PersonasContactos> PersonasContactos { get; set; }
		public List<PersonasEducacion> PersonasEducacion { get; set; }
		public List<PersonasRegimenSalud> PersonasRegimenSalud { get; set; }

		public List<DeclaracionBienes> DeclaracionBienes { get; set; }
		public List<DeclaracionCausasDesplazamiento> DeclaracionCausasDesplazamiento { get; set; }
		public List<DeclaracionDaniosFamilia> DeclaracionDaniosFamilia { get; set; }
		public List<DeclaracionEstados> DeclaracionEstados { get; set; }
		public List<DeclaracionFuentesIngreso> DeclaracionFuentesIngreso { get; set; }
		public List<DeclaracionObtencionAgua> DeclaracionObtencionAgua { get; set; }
		public List<DeclaracionPersonasAyuda> DeclaracionPersonasAyuda { get; set; }
		public List<DeclaracionPsicosocial> DeclaracionPsicosocial { get; set; }



		public List<Type> GetTypes()
		{
			return new List<Type>(new Type[] 
			{ 
				typeof(Declaracion),
				typeof(Personas),
				typeof(Programacion),
				typeof(SubTablas),
				typeof(DeclaracionEstados),
				typeof(PersonasContactos),
				typeof(PersonasEducacion),
				typeof(PersonasRegimenSalud)

			});
		}

    }
}
