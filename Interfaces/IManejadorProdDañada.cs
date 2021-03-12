using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    public interface IManejadorProdDañada:IManejadorGenerico<Produccion_Dañada>
    {
        //Colocar operaciones espeficicas de Produccion dañada.
        IEnumerable<VistaProduccionDañada> VisualizarDañados { get; }
        IEnumerable<VistaProduccionDañada> VisualizarDañadosPorFechas(DateTime fechainicio, DateTime fechafin);
    }
}
