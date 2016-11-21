using System;
namespace Herramienta.Modelos.Extensiones
{
	public static class BoolUtil
	{
		public static void LanzarExcepcionCuandoNoCumple(this bool value, string mensaje)
		{
			if (!value)
				throw new Exception(mensaje);
		}

		public static void SiCumpleEntonces(this bool value, Action ejecutarSi)
		{
			if (value) ejecutarSi();
		}
	}
}

