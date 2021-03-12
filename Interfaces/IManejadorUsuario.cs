using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    /// <summary>
    /// Proporciona los metodos relacionados a los usuarios
    /// </summary>
    public interface IManejadorUsuario:IManejadorGenerico<Usuario>
    {
        /// <summary>
        /// Verifica si las credenciales son validas para el usuario
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="clave">Contraseña del usuario.</param>
        /// <returns>Si las credenciales son correctas regresa el usuario completo, de otro modo regresa null</returns>
        Usuario login(string usuario, string clave);
        Usuario BuscarPorNombre(String usuario);
    }
}
