using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Usuario:Base
    {
        public string usuario { get; set; }
        public string clave { get; set; }
        public Int16 rol { get; set; }
    }
}
