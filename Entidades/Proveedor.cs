using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Proveedor:Base
    {
        public string ruc_prov { get; set; }
        public string nombre_prov { get; set; }
        public string direccion_prov { get; set; }
        public string telefono_prov { get; set; }
    }
}
