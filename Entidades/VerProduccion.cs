using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class VerProduccion:Base
    {
        public DateTime fecha { get; set; }
        public string ProductoAnterior { get; set; }
        public string ProductoNuevo { get; set; }
        public string Empleado { get; set; }
        public string TipoProduccion { get; set; }
        public int Cantidad { get; set; }
        public string Observaciones { get; set; }
    }
}
