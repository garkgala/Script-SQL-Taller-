using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    public interface IManejadorAsistencias:IManejadorGenerico<Asistencia>
    {
        //Colocar operaciones especificas de asistencias.
        bool validarAsistencia(Empleado emp);

        /// <summary>
        /// Registra una nueva asistencia validando que no exista una anterior para el dia indicado.
        /// </summary>
        /// <param name="entidad">Empleado que esta registrando su asistencia</param>
        /// <param name="tipo">Este tipo debe ser "entrada" o "salida", en base a ello se realizará la validacion.</param>
        /// <returns></returns>
        bool registrar_asistencia(Empleado entidad, string tipo);
        /// <summary>
        /// Para editar una asistencia en el modo administrador, este permite editar todo y no solo la fecha/hora de salida. 
        /// </summary>
        /// <param name="anterior">Asistencia que sera modificada</param>
        /// <param name="nueva">Asistencia con los datos modificados</param>
        /// <returns></returns>
        bool editar_todo(Asistencia entidad);
        IEnumerable<Consulta_asistencia> buscarPorfecha(DateTime fechainicio, DateTime fechafinal);
        IEnumerable<Asistencia> buscarfecha(DateTime fecha);
        IEnumerable<Consulta_asistencia> consultar_asistencias { get; }

    }
}
