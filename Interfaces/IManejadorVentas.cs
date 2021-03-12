using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    /// <summary>
    /// Proporciona los metodos relacionados con Ventas.
    /// </summary>
    public interface IManejadorVentas:IManejadorGenerico<Venta>
    {
        /// <summary>
        /// Devuelve todas las ventas realizadas en un intervalo de tiempo
        /// </summary>
        /// <param name="inicio">Fecha de inicio del intervalo.</param>
        /// <param name="fin">Fecha de fin del intervalo de la consulta.</param>
        /// <returns>Conjunto de ventas efectuadas en el intervalo ingresado. </returns>
        IEnumerable<ventasdetalle> VentasEnIntervalo(DateTime inicio, DateTime fin);
        /// <summary>
        /// Devuelve todas las ventas realizadas a un cliente especifico en un intervalo de tiempo.
        /// </summary>
        /// <param name="id_cli">Cliente al que se realizaron las ventas</param>
        /// <param name="inicio">Fecha de inicio del intervalo.</param>
        /// <param name="fin">Fecha de fin del intervalo de la consulta.</param>
        /// <returns>Conjunto de ventas efectuadas al cliente en el intervalo ingresado.</returns>
        IEnumerable<Venta> VentasDeClientteEnIntervalo(int id_cli, DateTime inicio, DateTime fin);
        /// <summary>
        /// Valida si la factura existe en la tabla ventas.
        /// </summary>
        /// <param name="n_factura">Numero de factura a validar.</param>
        /// <returns>Devuelve true si la factura existe en la BDD, de lo contrario devuelve false.</returns>
        bool validar_factura(string n_factura);
        /// <summary>
        /// Muestra los datos de las ventas con los nombres de los producto y clientes (sin los id como esta almacenado)
        /// </summary>
        /// <returns>devuelve todas las ventas realizadas</returns>
        IEnumerable<ventasdetalle> VentasEnDetalle { get; }

    }
}
