using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Comun.Interfaces
{
    public interface Idb
    {
        /// <summary>
        /// Captura los errores generados en la base de datos.
        /// </summary>
        string Error { get; }
        bool Comando(SqlCommand command);
        object Consulta(string consulta);
    }
}
