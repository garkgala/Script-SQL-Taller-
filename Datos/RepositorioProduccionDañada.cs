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
    public class RepositorioProduccionDañada : IManejadorProdDañada
    {

        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Produccion_Dañada> validador;
        public RepositorioProduccionDañada(AbstractValidator<Produccion_Dañada> v)
        {
            this.validador = v;
            db = new Basededatos();

        }
        public IEnumerable<Produccion_Dañada> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from produccion_dañada");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Produccion_Dañada> datos = new List<Produccion_Dañada>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Produccion_Dañada dato = new Produccion_Dañada
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                fecha = Convert.ToDateTime(dr[1].ToString()),
                                nombre_emp = dr[2].ToString(),
                                id_prod = Convert.ToInt32(dr[3].ToString()),
                                cantidad = Convert.ToInt32(dr[4].ToString())
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

        public IEnumerable<VistaProduccionDañada> VisualizarDañados
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from vw_vista_dañados");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<VistaProduccionDañada> datos = new List<VistaProduccionDañada>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            VistaProduccionDañada dato = new VistaProduccionDañada
                            {
                                Fecha = Convert.ToDateTime(dr[0].ToString()).ToShortDateString(),
                                Responsable = dr[1].ToString(),
                                Producto = dr[2].ToString(),
                                Cantidad = Convert.ToInt32(dr[3].ToString())
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

        public Produccion_Dañada BuscarPorId(string id)
        {
            try
            {
                Produccion_Dañada dato = new Produccion_Dañada();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM produccion_dañada WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.fecha = Convert.ToDateTime(dr[1].ToString());
                        dato.nombre_emp = dr[2].ToString();
                        dato.id_prod = Convert.ToInt32(dr[3].ToString());
                        dato.cantidad = Convert.ToInt32(dr[4].ToString());
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

        public bool crear(Produccion_Dañada entidad)
        {
            try
            {
                string sql = "INSERT INTO produccion_dañada VALUES (" +
                    "'"+entidad.fecha.ToString("MM/dd/yyyy")+"', " +
                    "'"+entidad.nombre_emp+"', " +
                    ""+entidad.id_prod+", " +
                    ""+entidad.cantidad+")";
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

        public bool editar(Produccion_Dañada entidadanterior, Produccion_Dañada entidadmodificada)
        {
            try
            {
                string sql = "UPDATE produccion_dañada SET " +
                    "fecha='"+entidadmodificada.fecha+"', " +
                    "nombre_emp='"+entidadmodificada.nombre_emp+"', " +
                    "id_prod="+entidadmodificada.id_prod+", " +
                    "cantidad="+entidadmodificada.cantidad+" WHERE id=" + entidadanterior.id;
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

        public bool eliminar(Produccion_Dañada entidad)
        {
            try
            {
                string sql = "DELETE FROM produccion_dañada WHERE id=" + entidad.id;
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

        public IEnumerable<Produccion_Dañada> query(Expression<Func<Produccion_Dañada, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public IEnumerable<VistaProduccionDañada> VisualizarDañadosPorFechas(DateTime fechainicio, DateTime fechafin)
        {
            return VisualizarDañados.Where(p => Convert.ToDateTime(p.Fecha) >= fechainicio & Convert.ToDateTime(p.Fecha) <= fechafin);
        }
    }
}
