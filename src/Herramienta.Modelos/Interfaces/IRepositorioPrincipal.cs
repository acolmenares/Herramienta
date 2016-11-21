using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Herramienta.Modelos.Interfaces
{
	public interface IRepositorioPrincipal:IDisposable
	{
		/// <summary>
		/// Devolver Lista de elementos que cumplen la condicion dada en el predicato
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="predicado"></param>
		/// <returns></returns>
		List<T> Consultar<T>(Expression<Func<T, bool>> predicado) where T : IEntidad;

		/// <summary>
		/// Conultar de una Entidad por su id ( puede ser null)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
		T Consultar<T>(int id) where T : IEntidad;

		/// <summary>
		/// Consulta todos los elementos
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		List<T> Consultar<T>() where T : IEntidad;


		List<From> Consultar<From>(ISelector<From> selector) where From:IEntidad;

		List<Into> Consultar<From, Into>(ISelector<From> selector) where From : IEntidad;

		/// <summary>
		/// Devuelve el primer elemento que cumple con el predicado ( puede ser null)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="predicado"></param>
		/// <returns></returns>
		T ConsultarSimple<T>(Expression<Func<T, bool>> predicado) where T : IEntidad;

		/// <summary>
		/// Actualiza las propieades de la entidad tipo T establecidas en onlyFieds de acuerdo con el predicado
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="predicate"></param>
		/// <param name="onlyFields"></param>
		/// <returns>Numero de registros actualizados</returns>
		int Actualizar<T>(T data, Expression<Func<T, object>> onlyFields, Expression<Func<T, bool>> predicate) where T : IEntidad;

		/// <summary>
		/// Actualiza  las propieades de la entidad tipo T establecidad en onlyFieds de acuerdo con el Id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="onlyFields"></param>
		/// <returns>Numero de registros actualizados (0 o 1 ) </returns>
		int Actualizar<T>(T data, Expression<Func<T, object>> onlyFields) where T : IEntidad;


		/// <summary>
		/// Actualiza todas la propiedades de la Entidad T de acuerdo con el predicado
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="predicate"></param>
		/// <returns>Numero de registros actualizados</returns>
		int Actualizar<T>(T data, Expression<Func<T, bool>> predicate) where T : IEntidad;

		/// <summary>
		/// Actualiza todas las propiedades de la Entidad T de acuero con el Id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns>Numero de registros actualizados (0 o 1)</returns>
		int Actualizar<T>(T data) where T : IEntidad;

		/// <summary>
		/// Inserta un registro del tipo T
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		void Crear<T>(T data) where T : IEntidad;

		/// <summary>
		/// Borra un registro del Tipo T por el valor de id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id"></param>
		/// <returns>Numero de registros borrados(0 o 1)</returns>
		int Borrar<T>(int id) where T : IEntidad;

		/// <summary>
		/// Borra registro del tipo T de acuerd con el valor de Id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns>Numero de registros borrados (0 o 1)</returns>
		int Borrar<T>(T data) where T : IEntidad;

		/// <summary>
		/// Borrar registros de acuerdo con el predicado
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="predicado"></param>
		/// <returns>Numero de registros borrados</returns>
		int Borrar<T>(Expression<Func<T, bool>> predicado) where T : IEntidad;

		/// <summary>
		/// Si el respositorio acepta transacciones inicia una
		/// </summary>
		void IniciarTransaccion();

		/// <summary>
		/// Finaliza la transaccion guardando cambios( commit)
		/// </summary>
		void FinalizarTransaccion();

		/// <summary>
		/// Cancela la transaccion ignorando los cambios ( rollback)
		/// </summary>
		void CancelarTransaccion();

		/*
        SqlExpression<T> CrearSqlExpression<T>();

        List<dynamic> Consultar<T>(SqlExpression<T> consulta);
        */

	}
}

