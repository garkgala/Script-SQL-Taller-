using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Producto_Perdido:Base
    {
        public Int64 id_prod { get; set; }
        public string registrado_por { get; set; }
        public DateTime fecha { get; set; }
        public int cantidad { get; set; }
        public string observaciones { get; set; }
    }
}
