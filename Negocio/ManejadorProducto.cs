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
    public class ManejadorProducto : IManejadorProducto
    {
        RepositorioProductos repo;
        public ManejadorProducto()
        {
            repo = new RepositorioProductos(new ValidadorProducto());
        }
        public IEnumerable<Producto> leer
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

        public Producto BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public Producto BuscarPorNombreExacto(string criterio)
        {
            return repo.BuscarPorNombreExacto(criterio);
        }

        public IEnumerable<Producto> BuscarProductoPorNombre(string criterio)
        {
            return repo.BuscarProductoPorNombre(criterio);
        }

        public bool crear(Producto entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Producto entidadanterior, Producto entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Producto entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Producto> query(Expression<Func<Producto, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
