using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    public interface IManejadorProdPerdido:IManejadorGenerico<Producto_Perdido>
    {
        //Colocar operaciones espeficicas de productos perdidos.
        IEnumerable<VistaProductoPerdido> VisualizarPerdidos { get; }
        IEnumerable<VistaProductoPerdido> VisualizarPerdidosPorFecha(DateTime fechainicio, DateTime fechafin);
    }
}
