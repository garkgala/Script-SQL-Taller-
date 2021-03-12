using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class consulta_compra
    {
        public DateTime fecha { get; set; }
        public string nombre_prov { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public double precio  { get; set; }
        public double  total { get; set; }
    }
}
