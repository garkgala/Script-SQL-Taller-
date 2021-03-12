using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Empleado:Base
    {
        public string dni { get; set; }
        public string nombre_emp { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", nombre_emp);
        }
        public string direccion_emp { get; set; }
        public string telefono_emp { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string cargo { get; set; }
        public string tipo_cargo { get; set; }
        public string tipo_pago { get; set; }
        public double sueldo { get; set; }
    }
}
