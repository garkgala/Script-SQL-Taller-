using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    public interface IManejadorTiposProduccion:IManejadorGenerico<Tipos_Produccion>
    {
        //Colocar operaciones espeficicas de
        List<Tipos_Produccion> nombresTipo { get; }
    }
}
