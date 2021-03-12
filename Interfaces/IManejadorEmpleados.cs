using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Interfaces
{
    public interface IManejadorEmpleados:IManejadorGenerico<Empleado>
    {
        //Aqui colocar algun operaciones especificas para empleados
        IEnumerable<Empleado> traerNombresEmpleados { get; }
        IEnumerable<Empleado> BuscarPorNombre(string nombre);
    }
}
