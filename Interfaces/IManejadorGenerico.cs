using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Comun.Interfaces
{
    /// <summary>
    /// Proporciona los metodos basicos (CRUD) de acceso a la tabla de base de datos.
    /// </summary>
    /// <typeparam name="T">Tipo de entdad (clase) a la que se refiere a la tabla de la base de datos.</typeparam>
    public interface IManejadorGenerico<T> where T:Base
    {
        /// <summary>
        /// Metodo para insertar datos
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Confirmacion del ingreso de la entidad</returns>
        bool crear(T entidad);
        /// <summary>
        /// Obtiene todos los registros de la tabla de la base de datos.
        /// </summary>
        IEnumerable<T> leer { get; } 
        /// <summary>
        /// Actualiza un registro en una tabla en base a su ID.
        /// </summary>
        /// <param name="id">Debe ser el mismo ID que se encuentra registrado en la base de datos.</param>
        /// <param name="entidadmodificada">Entidad ya actualizada que se va a registrar en la tabla.</param>
        /// <returns>Confirmacion de al actualizacion de un registro.</returns>
        bool editar(T entidadanterior, T entidadmodificada);
        /// <summary>
        /// Elimina un registro en una tabla de la base de datos.
        /// </summary>
        /// <param name="id">Id del registro a eliminar (debe ser el mismo que se encuentra en la tabla de la BDD)</param>
        /// <returns>Confirmacion de la eliminacion del registro.</returns>
        bool eliminar(T entidad);
        /// <summary>
        /// Proporciona informacion sobre el error generado en alguna operacion
        /// </summary>
        string Error { get; }

        //Query-> para realizar consultas de acuerdo a la tabla, mediante expresiones lambda.
        //como va a devolver un conjunto de elementos debe ser ienumerable.
        /// <summary>
        /// Realiza una consulta personailzada a la tabla mediante expresiones lambda
        /// </summary>
        /// <param name="predicado">Expresion lambda que define la consulta</param>
        /// <returns>Conjunto de entidades que cumplen con la consulta</returns>
        IEnumerable<T> query(Expression<Func<T, bool>> predicado);
        /// <summary>
        /// Obtener una entidad en base a su id.
        /// </summary>
        /// <param name="id">Id de la entidad que debe ser el mismo que se encuentra en la tabla de la BDD.</param>
        /// <returns>Entidad completa que corresponde al id ingresado.</returns>
        T BuscarPorId(string id);
    }
}
