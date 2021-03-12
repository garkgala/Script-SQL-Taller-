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
    public class ManejadorUsuario : IManejadorUsuario
    {
        RepositorioUsuario repo;
        public ManejadorUsuario()
        {
            repo = new RepositorioUsuario(new ValidadorUsuario());
        }
        public IEnumerable<Usuario> leer
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

        public Usuario BuscarPorId(string id)
        {
            return repo.BuscarPorId(id);
        }

        public Usuario BuscarPorNombre(String usuario)
        {
            return repo.BuscarPorNombre(usuario);
        }

        public bool crear(Usuario entidad)
        {
            return repo.crear(entidad);
        }

        public bool editar(Usuario entidadanterior, Usuario entidadmodificada)
        {
            return repo.editar(entidadanterior, entidadmodificada);
        }

        public bool eliminar(Usuario entidad)
        {
            return repo.eliminar(entidad);
        }

        public Usuario login(string usuario, string clave)
        {
            return repo.login(usuario, clave);
        }

        public IEnumerable<Usuario> query(Expression<Func<Usuario, bool>> predicado)
        {
            return repo.query(predicado);
        }
    }
}
