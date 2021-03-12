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
    public class ManejadorProduccion : IManejadorProduccion
    {
        RepositorioProduccion repo;
        public ManejadorProduccion()
        {
            repo = new RepositorioProduccion(new ValidadorProduccion());
        }
        public IEnumerable<Produccion> leer
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

        public IEnumerable<VerProduccion> ListarProduccion
        {
            get
            {
                return repo.ListarProduccion;
            }
        }

        public IEnumerable<Calcular_Produccion> CalcularProduccion
        {
            get
            {
                return repo.CalcularProduccion;
            }
        }

        public Produccion BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public IEnumerable<Calcular_Produccion> CalcularProduccionporfechas(DateTime fechainicio, DateTime fechafin)
        {
            return repo.CalcularProduccionporfechas(fechainicio, fechafin);
        }

        public bool crear(Produccion entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Produccion entidadanterior, Produccion entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Produccion entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Produccion> query(Expression<Func<Produccion, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
