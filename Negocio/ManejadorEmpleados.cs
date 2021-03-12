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
    public class ManejadorEmpleados : IManejadorEmpleados
    {
        RepositorioEmpleados repo;
        public ManejadorEmpleados()
        {
            repo = new RepositorioEmpleados(new ValidadorEmpleado());
        }
        public IEnumerable<Empleado> leer
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

        public IEnumerable<Empleado> traerNombresEmpleados
        {
            get
            {
                return repo.traerNombresEmpleados;
            }
        }

        public Empleado BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public IEnumerable<Empleado> BuscarPorNombre(string nombre)
        {
            return repo.BuscarPorNombre(nombre);
        }

        public bool crear(Empleado entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Empleado entidadanterior, Empleado entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Empleado entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Empleado> query(Expression<Func<Empleado, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
