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
    public class ManejadorProduccionDañada : IManejadorProdDañada
    {
        RepositorioProduccionDañada repo;
        public ManejadorProduccionDañada()
        {
            repo = new RepositorioProduccionDañada(new ValidadorProduccionDañada());
        }
        public IEnumerable<Produccion_Dañada> leer
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

        public IEnumerable<VistaProduccionDañada> VisualizarDañados
        {
            get
            {
                return repo.VisualizarDañados;
            }
        }

        public Produccion_Dañada BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public bool crear(Produccion_Dañada entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Produccion_Dañada entidadanterior, Produccion_Dañada entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Produccion_Dañada entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Produccion_Dañada> query(Expression<Func<Produccion_Dañada, bool>> predicado)
        {
            return repo.query(predicado);
        }

        public IEnumerable<VistaProduccionDañada> VisualizarDañadosPorFechas(DateTime fechainicio, DateTime fechafin)
        {
            return repo.VisualizarDañadosPorFechas(fechainicio, fechafin);
        }
    }
}
