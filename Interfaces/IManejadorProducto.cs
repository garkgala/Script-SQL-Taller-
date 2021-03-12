using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    /// <summary>
    /// Proporciona los metodos relacionados con los productos.
    /// </summary>
    public interface IManejadorProducto:IManejadorGenerico<Producto>
    {
        /// <summary>
        /// Realiza una busqueda a medida que se va escribiendo el nombre.
        /// </summary>
        /// <param name="criterio">nombre del producto a buscar.</param>
        /// <returns>Un conjunto de valores (productos) que coinciden con la busqueda ingresada.</returns>
        IEnumerable<Producto> BuscarProductoPorNombre(string criterio);
        /// <summary>
        /// Busca un roducto por su nombre exacto.
        /// </summary>
        /// <param name="criterio">Nombre excato del producto tal cual esta en la BDD.</param>
        /// <returns>Un producto que coincide con el nombre ingresado.</returns>
        Producto BuscarPorNombreExacto(string criterio);
    }
}
