using System;
using System.Collections.Generic;
using System.Text;

namespace Comun.Entidades
{
    public class Consulta_asistencia
    {
        public string Empleado { get; set; }
        public DateTime fecha_entrada { get; set; }
        public DateTime hora_entrada { get; set; }
        public DateTime fecha_salida { get; set; }
        public DateTime hora_salida { get; set; }
        public string estado { get; set; }
    }
}
