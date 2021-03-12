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
    public class RepositorioCliente : IManejadorClientes
    {
        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Cliente> validador;
        public RepositorioCliente(AbstractValidator<Cliente> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Cliente> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from clientes");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Cliente> datos = new List<Cliente>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Cliente dato = new Cliente
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                ruc_cli = dr[1].ToString(),
                                nombre_cli = dr[2].ToString(),
                                direccion_cli = dr[3].ToString(),
                                telefono_cli = dr[4].ToString()
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

        public Cliente BuscarPorId(string id)
        {
            try
            {
                Cliente dato = new Cliente();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM clientes WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.ruc_cli = dr[1].ToString();
                        dato.nombre_cli = dr[2].ToString();
                        dato.direccion_cli = dr[3].ToString();
                        dato.telefono_cli = dr[4].ToString();
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

        public bool crear(Cliente entidad)
        {
            string sql = "INSERT INTO clientes VALUES('" + entidad.ruc_cli + "', " +
                "'" + entidad.nombre_cli + "','" + entidad.direccion_cli + "', '" + entidad.telefono_cli + "')";
            try
            {
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

        public bool editar(Cliente entidadanterior, Cliente entidadmodificada)
        {
            try
            {
                string sql = "UPDATE clientes SET ruc_cli='"+ entidadmodificada.ruc_cli+"', " +
                    "nombre_cli='"+entidadmodificada.nombre_cli+"', " +
                    "direccion_cli='"+entidadmodificada.direccion_cli+"', " +
                    "telefono_cli='"+entidadmodificada.telefono_cli+"' " +
                    "WHERE id=" + entidadanterior.id;
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

        public bool eliminar(Cliente entidad)
        {
            try
            {
                string sql = "DELETE FROM clientes WHERE id=" + entidad.id;
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

        public IEnumerable<Cliente> query(Expression<Func<Cliente, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }
    }
}
