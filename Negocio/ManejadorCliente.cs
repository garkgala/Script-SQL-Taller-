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
    public class ManejadorCliente : IManejadorClientes
    {
        RepositorioCliente repo;
        public ManejadorCliente()
        {
            repo = new RepositorioCliente(new ValidadorCliente());
        }
        public IEnumerable<Cliente> leer
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

        public Cliente BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public bool crear(Cliente entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Cliente entidadanterior, Cliente entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Cliente entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Cliente> query(Expression<Func<Cliente, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
