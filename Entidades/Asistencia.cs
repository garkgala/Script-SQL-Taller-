using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Asistencia:Base
    {
        public Int64 id_emp { get; set; }
        public DateTime fecha_entrada { get; set; }
        public DateTime hora_entrada { get; set; }
        public DateTime hora_salida { get; set; }
        public DateTime fecha_salida { get; set; }
        public string estado { get; set; }

    }
}
