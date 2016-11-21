using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Herramienta.Modelos.Interfaces;

namespace Herramienta.Modelos.Entidades
{
    public class Programacion: IEntidad
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Id_Regional { get; set; }
        public string Numero { get; set; }
        public int? Id_Estado { get; set; }
        public int? Id_Grupo { get; set; }
        public int? Id_LugarEntrega { get; set; }
        public int? Id_TipoEntrega { get; set; }
        public int? Id_Bodega { get; set; }

    }
}
