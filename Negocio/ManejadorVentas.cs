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
    public class ManejadorVentas : IManejadorVentas
    {
        RepositorioVentas repo;
        public ManejadorVentas()
        {
            repo = new RepositorioVentas(new ValidadorVenta());
        }
        public IEnumerable<Venta> leer
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

        public IEnumerable<ventasdetalle> VentasEnDetalle
        {
            get
            {
                return repo.VentasEnDetalle;
            }
        }

        public Venta BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public bool crear(Venta entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Venta entidadanterior, Venta entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Venta entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Venta> query(Expression<Func<Venta, bool>> predicado)
        {
            return repo.query(predicado);
        }

        public bool validar_factura(string n_factura)
        {
            return repo.validar_factura(n_factura);
        }

        public IEnumerable<Venta> VentasDeClientteEnIntervalo(int id_cli, DateTime inicio, DateTime fin)
        {
            return repo.VentasDeClientteEnIntervalo(id_cli, inicio, fin);
        }

        public IEnumerable<ventasdetalle> VentasEnIntervalo(DateTime inicio, DateTime fin)
        {
            return repo.VentasEnIntervalo(inicio, fin);
        }
    }
}
