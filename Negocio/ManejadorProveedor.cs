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
    public class ManejadorProveedor : IManejadorProveedor
    {
        RepositorioProveedores repo;
        public ManejadorProveedor()
        {
            repo = new RepositorioProveedores(new ValidadorProveedor());
        }
        public IEnumerable<Proveedor> leer
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

        public Proveedor BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public bool crear(Proveedor entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Proveedor entidadanterior, Proveedor entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Proveedor entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Proveedor> query(Expression<Func<Proveedor, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
