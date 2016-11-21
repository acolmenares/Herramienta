using System;
using Herramienta.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace Herramienta.Modelos.Entidades
{
	[Alias("Personas")]
	public class Personas: IEntidad, IConIdDeclaracion
    {
        public int Id { get; set; }
        public int? Id_Declaracion { get; set; }
        public string Tipo { get; set; }
        public int? Id_Tipo_Identificacion { get; set; }
        public string Identificacion { get; set; }
        public string Primer_Nombre { get; set; }
        public string Segundo_Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }
        public int? Edad { get; set; }
        public int? Id_Genero { get; set; }
        public int? Id_Jefe_Hogar { get; set; }
        public int? Id_Parentesco { get; set; }
        public int? Id_Discapacitado { get; set; }
        public int? Id_Estudia_Antes { get; set; }
        public int? Id_Estudia_Actualmente { get; set; }
        public int? Id_Ultimo_Grado { get; set; }
        public string Institucion_Estudia { get; set; }
        public int? Id_Motivo_NoEstudio { get; set; }
        public int? Id_Tipo_Discapacidad { get; set; }
        public int? Id_Grupo_Etnico { get; set; }
        public int? Id_Embarazada { get; set; }
        public int? Id_Lactando { get; set; }
        public int? Id_Solicito_Atencion_Psicologica { get; set; }
        public int? Id_Recibio_Atencion_Psicologica { get; set; }
        public string Quien_Atencion_Psicologica { get; set; }
        public int? Id_Recibio_Tratamiento { get; set; }
        public int? Id_Causas_Discapacidad { get; set; }
        public int? Id_Sabe_Leer_Escribir { get; set; }
        public string Institucion_Estudiaba { get; set; }
        public int? Id_Paga_Matricula { get; set; }
        public int? Id_Tipo_Apoyo_Educativo { get; set; }
        public int? Id_Problemas_Establecimiento { get; set; }
        public int? Id_Cabeza_Hogar { get; set; }
        public int? Edad_Semanas { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public bool? Cierre { get; set; }
        public int? Id_Tipo_Miembro { get; set; }
        public int? Id_Regimen_Salud_Antes { get; set; }
        public int? Id_Eps_Antes { get; set; }
        public int? Id_Regimen_Salud_Despues { get; set; }
        public int? Id_Eps_Despues { get; set; }
        public string Municipio_Afiliacion_Antes { get; set; }
        public string Municipio_Afiliacion_Despues { get; set; }
        public int? Id_Afectado_Desplazamiento { get; set; }
        public int? Id_Certificado { get; set; }
        public int? Id_Municipio_Instituto { get; set; }
        public int? Id_Municipio_Instituto_Actual { get; set; }
    }
}
