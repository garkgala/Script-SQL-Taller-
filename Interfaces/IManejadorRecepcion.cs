using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    public interface IManejadorRecepcion:IManejadorGenerico<Recepcion>
    {
        //Colocar operaciones espeficicas de recepcion.
        IEnumerable<consulta_compra> consultarCompras { get; }
        IEnumerable<consulta_compra> ComprasPorFechas(DateTime inicio, DateTime fin);
    }
}
