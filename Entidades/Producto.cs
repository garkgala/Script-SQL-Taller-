using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Producto : Base
    {
        public string descripcion { get; set; }
        public string tipo_producto { get; set; }
        public Int64 cantidad { get; set; }
        public double precio { get; set; }
        public double? total {get; set;}
    }
}
