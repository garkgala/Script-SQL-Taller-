using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    public interface IManejadorProduccion:IManejadorGenerico<Produccion>
    {
        //Colocar operaciones espeficicas de produccion.
        IEnumerable<VerProduccion> ListarProduccion { get; }
        /// <summary>
        /// Devuelve una lista enumerada de la produccion por fecha y el calculo total (a pagar) por cada uno.
        /// </summary>
        IEnumerable<Calcular_Produccion> CalcularProduccion { get; }
        IEnumerable<Calcular_Produccion> CalcularProduccionporfechas(DateTime fechainicio, DateTime fechafin);
    }
}
