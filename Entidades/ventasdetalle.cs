using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class ventasdetalle:Base
    {
        public DateTime fecha_venta { get; set; }
        public string nombre_cli { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public double precio_venta { get; set; }
        public double Total { get; set; }
    }
}
