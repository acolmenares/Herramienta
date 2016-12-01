using Herramienta.Modelos.Objetivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Herramienta.Modelos.Extensiones
{
    public static class RecibeRacionUtil
    {
        public static bool TengoPeridoRadicacionAnterior( this RecibeRacion yo, RecibeRacion otro)
        {
            return   string.Compare(yo.Periodo, otro.Periodo)<=0 && string.Compare(yo.ConcatenarAnioPeriodo(), otro.ConcatenarAnioPeriodo())==-1;
                        
        }

        public static bool SoyMismoPeridoRadicacionConMesAnterior(this RecibeRacion yo, RecibeRacion otro)
        {
            return (
                string.Compare(yo.Periodo, otro.Periodo) == 0
                && string.Compare(yo.AnioMes, otro.AnioMes) == -1
                );

            //return true;
        }

        static string ConcatenarAnioPeriodo(this RecibeRacion obj)
        {
            return string.Format("{0}{1}", obj.AnioMes.ExtraerAnio(), obj.Periodo);
        }


        static string ExtraerAnio( this string aniomes)
        {
            return (!string.IsNullOrEmpty(aniomes) && aniomes.Length > 4) ? aniomes.Substring(0,4) : "";
        }
    }
}
