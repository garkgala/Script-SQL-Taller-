using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Cliente:Base
    {
        public string ruc_cli { get; set; }
        public string nombre_cli { get; set; }
        public string direccion_cli { get; set; }
        public string telefono_cli { get; set; }
    }
}
