using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using System.Data.SqlClient;
using System.Linq;
using Comun.Entidades;
using Comun.Interfaces;
using Comun.Validadores;
using System.Linq.Expressions;
using System.Data;

namespace Datos
{
    public class RepositorioUsuario : IManejadorUsuario
    {
        Basededatos db;
        AbstractValidator<Usuario> validador;
        public string Error { get; private set; }
        public RepositorioUsuario(AbstractValidator<Usuario> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Usuario> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from usuarios");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Usuario> datos = new List<Usuario>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Usuario dato = new Usuario
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                usuario = dr[1].ToString(),
                                clave = dr[2].ToString(),
                                rol = Convert.ToSByte(dr[3].ToString())
                            };
                            datos.Add(dato);
                        }
                        dr.Close();
                        db.Desconectar();
                    }
                    db.Desconectar();
                    Error = "";
                    return datos;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    return null;
                }
            }
        }

        public Usuario BuscarPorId(string id)
        {
            try
            {
                Usuario dato = new Usuario();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM usuarios WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.usuario = dr[1].ToString();
                        dato.clave = dr[2].ToString();
                        dato.rol = Convert.ToSByte(dr[3].ToString());
                    }
                }
                db.Desconectar();
                Error = "";
                return dato;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        public bool crear(Usuario entidad)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_n_usuario", db.conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@usuario", entidad.usuario);
                cmd.Parameters.AddWithValue("@clave", entidad.clave);
                cmd.Parameters.AddWithValue("@rol", entidad.rol);
                db.Conectar();
                db.Comando(cmd);
                db.Desconectar();
                Error = "";
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public bool editar(Usuario entidadanterior, Usuario entidadmodificada)
        {
            try
            {
                string sql = "UPDATE usuarios SET clave='"+entidadmodificada.clave+"' WHERE id=" + entidadmodificada.id;
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                db.Conectar();
                db.Comando(cmd);
                db.Desconectar();
                Error = "";
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public bool eliminar(Usuario entidad)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_el_usuario", db.conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(entidad.id.ToString()));
                db.Conectar();
                db.Comando(cmd);
                db.Desconectar();
                Error = "";
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public Usuario login(string usuario, string clave)
        {
            //Usuario user = leer.Where(a => a.usuario == usuario & a.clave == clave).SingleOrDefault();
            Usuario user = query(us => us.usuario == usuario && us.clave == clave).SingleOrDefault();
            return user;
        }

        public IEnumerable<Usuario> query(Expression<Func<Usuario, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public Usuario BuscarPorNombre(String usuario)
        {
            Usuario user = leer.Where(p => p.usuario == usuario).FirstOrDefault();
            return user;
        }
    }
}
