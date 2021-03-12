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
    public class RepositorioTiposProduccion : IManejadorTiposProduccion
    {
        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Tipos_Produccion> validador;
        public RepositorioTiposProduccion(AbstractValidator<Tipos_Produccion> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Tipos_Produccion> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from tipos_produccion");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Tipos_Produccion> datos = new List<Tipos_Produccion>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Tipos_Produccion dato = new Tipos_Produccion
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                tipo_produccion = dr[1].ToString()
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

        public List<Tipos_Produccion> nombresTipo
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select tipo_produccion from tipos_produccion");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Tipos_Produccion> datos = new List<Tipos_Produccion>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Tipos_Produccion dato = new Tipos_Produccion
                            {
                                tipo_produccion = dr[0].ToString()
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

        public Tipos_Produccion BuscarPorId(string id)
        {
            try
            {
                Tipos_Produccion dato = new Tipos_Produccion();
                db.Conectar();
                SqlDataReader r = (SqlDataReader)db.Consulta("SELECT * FROM tipos_produccion WHERE id=" + Convert.ToInt32(id));
                while (r.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(r[0].ToString());
                        dato.tipo_produccion = r[1].ToString();
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

        public bool crear(Tipos_Produccion entidad)
        {
            try
            {
                string sql = "INSERT INTO tipos_produccion VALUES('" + entidad.tipo_produccion + "')";
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

        public bool editar(Tipos_Produccion entidadanterior, Tipos_Produccion entidadmodificada)
        {
            try
            {
                string sql = "UPDATE tispo_produccion SET tipo_produccion='" + entidadmodificada.tipo_produccion + "' WHERE id=" + entidadanterior.id;
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

        public bool eliminar(Tipos_Produccion entidad)
        {
            try
            {
                string sql = "DELETE FROM tipos_produccion WHERE id=" + entidad.id;
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

        public IEnumerable<Tipos_Produccion> query(Expression<Func<Tipos_Produccion, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }
    }
}
