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
    public class ManejadorTiposProduccion : IManejadorTiposProduccion
    {
        RepositorioTiposProduccion repo;
        public ManejadorTiposProduccion()
        {
            repo = new RepositorioTiposProduccion(new ValidadorTiposProduccion());
        }
        public IEnumerable<Tipos_Produccion> leer
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

        public List<Tipos_Produccion> nombresTipo
        {
            get
            {
                return repo.nombresTipo;
            }
        }

        public Tipos_Produccion BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public bool crear(Tipos_Produccion entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Tipos_Produccion entidadanterior, Tipos_Produccion entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Tipos_Produccion entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Tipos_Produccion> query(Expression<Func<Tipos_Produccion, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
