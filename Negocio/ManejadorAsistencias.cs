using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Comun.Entidades;
using Comun.Interfaces;
using Datos;
using Comun.Validadores;
using System.Linq;

namespace Negocio
{
    public class ManejadorAsistencias : IManejadorAsistencias
    {
        RepositorioAsistencias repo;
        public ManejadorAsistencias()
        {
            repo = new RepositorioAsistencias(new ValidadorAsistencia());
        }
        public IEnumerable<Asistencia> leer
        {
            get
            {
                return repo.leer;
            }
        }

        public string Error 
        {
            get
            {
                return repo.Error;
            }
        }

        public IEnumerable<Consulta_asistencia> consultar_asistencias
        {
            get
            {
                return repo.consultar_asistencias;
            }
        }

        public IEnumerable<Asistencia> buscarfecha(DateTime fecha)
        {
            return repo.buscarfecha(fecha);
        }

        public IEnumerable<Consulta_asistencia> buscarPorfecha(DateTime fechainicio, DateTime fechafinal)
        {
            return repo.buscarPorfecha(fechainicio, fechafinal);
        }

        public Asistencia BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public bool crear(Asistencia entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Asistencia entidadanterior, Asistencia entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool editar_todo(Asistencia entidad)
        {
            return repo.editar_todo(entidad);
        }

        public bool eliminar(Asistencia entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Asistencia> query(Expression<Func<Asistencia, bool>> predicado)
        {
            return repo.query(predicado);
        }

        public bool registrar_asistencia(Empleado entidad, string tipo)
        {
            return repo.registrar_asistencia(entidad, tipo);
        }

        public bool validarAsistencia(Empleado emp)
        {
            return repo.validarAsistencia(emp);
        }
    }
}
