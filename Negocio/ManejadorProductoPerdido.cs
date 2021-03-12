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
    public class ManejadorProductoPerdido : IManejadorProdPerdido
    {
        RepositorioProductosPerdidos repo;
        public ManejadorProductoPerdido()
        {
            repo = new RepositorioProductosPerdidos(new ValidadorProductoPerdido());
        }
        public IEnumerable<Producto_Perdido> leer
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

        public Producto_Perdido BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public bool crear(Producto_Perdido entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Producto_Perdido entidadanterior, Producto_Perdido entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Producto_Perdido entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Producto_Perdido> query(Expression<Func<Producto_Perdido, bool>> predicado)
        {
            return repo.query(predicado);
        }

        public IEnumerable<VistaProductoPerdido> VisualizarPerdidosPorFecha(DateTime fechainicio, DateTime fechafin)
        {
            return repo.VisualizarPerdidosPorFecha(fechainicio, fechafin);
        }

        public IEnumerable<VistaProductoPerdido> VisualizarPerdidos
        {
            get
            {
                return repo.VisualizarPerdidos;
            }
        }
    }
}
