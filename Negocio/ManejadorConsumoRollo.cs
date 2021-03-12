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
    public class ManejadorConsumoRollo : IManejadorConsumoRollos
    {
        RepositorioConsumoRollo repo;
        public ManejadorConsumoRollo()
        {
            repo = new RepositorioConsumoRollo(new ValidadorConsumoRollo());
        }
        public IEnumerable<Consumo_rollo> leer
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

        public IEnumerable<VistaConsumos> visualizarConsumos
        {
            get
            {
                return repo.visualizarConsumos;
            }
        }

        public IEnumerable<VistaConsumos> BuscarConsumoPorFecha(DateTime fecha)
        {
            return repo.BuscarConsumoPorFecha(fecha);
        }

        public Consumo_rollo BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public bool consumir_rollo(Producto producto, int cantidad)
        {
            return repo.consumir_rollo(producto, cantidad);
        }

        public bool crear(Consumo_rollo entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Consumo_rollo entidadanterior, Consumo_rollo entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Consumo_rollo entidad)
        {
            return repo.eliminar(entidad);
        }

        public IEnumerable<Consumo_rollo> query(Expression<Func<Consumo_rollo, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
