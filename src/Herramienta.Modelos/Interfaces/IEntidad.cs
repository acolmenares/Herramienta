using System;
using ServiceStack.Model;

namespace Herramienta.Modelos.Interfaces
{
	/// <summary>
	/// Todas las Entidades de nuestra repositorio de declarantes deben tener un Id tipo int
	/// </summary>
	public interface IEntidad : IHasId<int>
	{
		new int Id { get; set; }
	}
}

