using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    public interface IManejadorConsumoRollos:IManejadorGenerico<Consumo_rollo>
    {
        //Colocar operaciones especificas de consumo de rollos.
        bool consumir_rollo(Producto producto, int cantidad);
        IEnumerable<VistaConsumos> BuscarConsumoPorFecha(DateTime fecha);
        IEnumerable<VistaConsumos> visualizarConsumos { get; }
    }
}
