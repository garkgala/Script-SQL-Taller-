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
    public class RepositorioProduccion : IManejadorProduccion
    {
        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Produccion> validador;
        public RepositorioProduccion(AbstractValidator<Produccion> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Produccion> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from produccion");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Produccion> datos = new List<Produccion>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Produccion dato = new Produccion
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                fecha_produccion = Convert.ToDateTime(dr[1].ToString()),
                                id_prod_ant = Convert.ToInt32(dr[2].ToString()),
                                id_prod_nuevo = Convert.ToInt32(dr[3].ToString()),
                                id_emp = Convert.ToInt32(dr[4].ToString()),
                                tipo_produccion = dr[5].ToString(),
                                cantidad = Convert.ToInt32(dr[6].ToString()),
                                observaciones = dr[7].ToString()
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

        public IEnumerable<VerProduccion> ListarProduccion
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from ver_produccion");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<VerProduccion> datos = new List<VerProduccion>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            VerProduccion dato = new VerProduccion
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                fecha = Convert.ToDateTime(dr[1].ToString()),
                                ProductoAnterior = dr[2].ToString(),
                                ProductoNuevo = dr[3].ToString(),
                                Empleado = dr[4].ToString(),
                                TipoProduccion = dr[5].ToString(),
                                Cantidad = Convert.ToInt32(dr[6].ToString()),
                                Observaciones = dr[7].ToString()
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

        public IEnumerable<Calcular_Produccion> CalcularProduccion
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from vw_calcular_produccion");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Calcular_Produccion> datos = new List<Calcular_Produccion>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Calcular_Produccion dato = new Calcular_Produccion
                            {
                                fecha = Convert.ToDateTime(dr[0].ToString()).ToShortDateString(),
                                producto = dr[1].ToString(),
                                empleado = dr[2].ToString(),
                                actividad = dr[3].ToString(),
                                cantidad = Convert.ToInt32(dr[4].ToString()),
                                sueldo = Convert.ToDouble(dr[5].ToString()),
                                total = Convert.ToDouble(dr[6].ToString()),
                                observaciones = dr[7].ToString()
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

        public Produccion BuscarPorId(string id)
        {
            try
            {
                Produccion dato = new Produccion();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM produccion WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.fecha_produccion = Convert.ToDateTime(dr[1].ToString());
                        dato.id_prod_ant = Convert.ToInt32(dr[2].ToString());
                        dato.id_prod_nuevo = Convert.ToInt32(dr[3].ToString());
                        dato.id_emp = Convert.ToInt32(dr[4].ToString());
                        dato.tipo_produccion = dr[5].ToString();
                        dato.cantidad = Convert.ToInt32(dr[6].ToString());
                        dato.observaciones = dr[7].ToString();
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

        public bool crear(Produccion entidad)
        {
            try
            {
                string sql = "INSERT INTO produccion VALUES('"+entidad.fecha_produccion.ToString("MM/dd/yyyy")+"', " +
                    "" + entidad.id_prod_ant + ", " +
                    "" + entidad.id_prod_nuevo + ", " +
                    "" + entidad.id_emp + ", " +
                    "'" + entidad.tipo_produccion + "', " +
                    "" + entidad.cantidad + ", " +
                    "'" + entidad.observaciones + "')";
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

        public bool editar(Produccion entidadanterior, Produccion entidadmodificada)
        {
            try
            {
                string sql = "UPDATE produccion SET fecha='" + entidadmodificada.fecha_produccion.ToString("MM/dd/yyyy") + "', " +
                    "id_prod_ant=" + entidadmodificada.id_prod_ant + ", " +
                    "id_prod_nuevo=" + entidadmodificada.id_prod_nuevo + ", " +
                    "id_emp=" + entidadmodificada.id_emp + ", " +
                    "tipo_produccion='" + entidadmodificada.tipo_produccion + "', " +
                    "cantidad=" + entidadmodificada.cantidad + ", " +
                    "Observaciones='" + entidadmodificada.observaciones + "' " +
                    "WHERE id= " + entidadanterior.id + "";
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

        public bool eliminar(Produccion entidad)
        {
            try
            {
                string sql = "DELETE FROM produccion WHERE id=" + entidad.id;
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

        public IEnumerable<Produccion> query(Expression<Func<Produccion, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public IEnumerable<Calcular_Produccion> CalcularProduccionporfechas(DateTime fechainicio, DateTime fechafin)
        {
            return CalcularProduccion.Where(p => Convert.ToDateTime(p.fecha) >= fechainicio & Convert.ToDateTime(p.fecha) <= fechafin);
        }
    }
}
