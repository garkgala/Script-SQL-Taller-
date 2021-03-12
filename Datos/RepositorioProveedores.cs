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
    public class RepositorioProveedores : IManejadorProveedor
    {
        Basededatos db;
        AbstractValidator<Proveedor> validador;

        public string Error { get; private set; }
        public RepositorioProveedores(AbstractValidator<Proveedor> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Proveedor> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from Proveedores");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Proveedor> datos = new List<Proveedor>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Proveedor dato = new Proveedor
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                ruc_prov = dr[1].ToString(),
                                nombre_prov = dr[2].ToString(),
                                direccion_prov = dr[3].ToString(),
                                telefono_prov = dr[4].ToString()
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

        public Proveedor BuscarPorId(string id)
        {
            try
            {
                Proveedor dato = new Proveedor();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM proveedores WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.ruc_prov = dr[1].ToString();
                        dato.nombre_prov = dr[2].ToString();
                        dato.direccion_prov = dr[3].ToString();
                        dato.telefono_prov = dr[4].ToString();
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

        public bool crear(Proveedor entidad)
        {
            try
            {
                string sql = "INSERT INTO proveedores VALUES ('"+entidad.ruc_prov+"', " +
                    "'"+entidad.nombre_prov+"', '"+entidad.direccion_prov+"'," +
                    " '"+entidad.telefono_prov+"')";
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

        public bool editar(Proveedor entidadanterior, Proveedor entidadmodificada)
        {
            try
            {
                string sql = "UPDATE proveedores SET ruc_prov='"+entidadmodificada.ruc_prov+"', " +
                    "nombre_prov='"+entidadmodificada.nombre_prov+"', " +
                    "direccion_prov='"+entidadmodificada.direccion_prov+"', " +
                    "telefono_prov='"+entidadmodificada.telefono_prov+"' " +
                    "WHERE id="+entidadanterior.id+"";
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

        public bool eliminar(Proveedor entidad)
        {
            try
            {
                string sql = "DELETE FROM proveedores WHERE id=" + entidad.id + "";
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

        public IEnumerable<Proveedor> query(Expression<Func<Proveedor, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }
    }
}
