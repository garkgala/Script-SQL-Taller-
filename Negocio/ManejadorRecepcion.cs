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
    public class ManejadorRecepcion : IManejadorRecepcion
    {
        RepositorioRecepcion repo;
        public ManejadorRecepcion()
        {
            repo = new RepositorioRecepcion(new ValidadorRecepcion());
        }
        public IEnumerable<Recepcion> leer
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

        public IEnumerable<consulta_compra> consultarCompras
        {
            get
            {
                return repo.consultarCompras;
            }
        }

        public Recepcion BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public IEnumerable<consulta_compra> ComprasPorFechas(DateTime inicio, DateTime fin)
        {
            return repo.ComprasPorFechas(inicio, fin);
        }

        public bool crear(Recepcion entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Recepcion entidadanterior, Recepcion entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Recepcion entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Recepcion> query(Expression<Func<Recepcion, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
