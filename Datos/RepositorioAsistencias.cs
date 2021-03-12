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
    public class RepositorioAsistencias:IManejadorAsistencias
    {
        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Asistencia> validador;
        public RepositorioAsistencias(AbstractValidator<Asistencia> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        /// <summary>
        /// Devuelve una lista de elementos(Asistencia) de la tabla de asisistencias de la BDD.
        /// </summary>
        public IEnumerable<Asistencia> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from asistencias");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Asistencia> datos = new List<Asistencia>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Asistencia dato = new Asistencia
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                id_emp = Convert.ToInt32(dr[1].ToString()),
                                fecha_entrada = Convert.ToDateTime(dr[2].ToString()),
                                hora_entrada = Convert.ToDateTime(dr[3].ToString()),
                                fecha_salida = Convert.ToDateTime(dr[4].ToString()),
                                hora_salida = Convert.ToDateTime(dr[5].ToString()),
                                estado = dr[6].ToString()
                            };
                            datos.Add(dato);
                        }
                        dr.Close();
                        db.Desconectar();
                    }
                    db.Desconectar();
                    Error = "";
                    return datos;
                }catch(Exception ex)
                {
                    Error = ex.Message;
                    return null;
                }
            }
        }

        public IEnumerable<Consulta_asistencia> consultar_asistencias
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from consulta_asistencias");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Consulta_asistencia> datos = new List<Consulta_asistencia>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Consulta_asistencia dato = new Consulta_asistencia
                            {
                                Empleado = dr[0].ToString(),
                                fecha_entrada = Convert.ToDateTime(dr[1].ToString()),
                                hora_entrada = Convert.ToDateTime(dr[2].ToString()),
                                fecha_salida = Convert.ToDateTime(dr[3].ToString()),
                                hora_salida = Convert.ToDateTime(dr[4].ToString()),
                                estado = dr[5].ToString()
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

        public bool crear(Asistencia entidad)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Edita las asistencias en base a la informacion contenida en entidadmodificada
        /// </summary>
        /// <param name="entidadanterior">Entidad a modificar</param>
        /// <param name="entidadmodificada">Entidad ya modificada.</param>
        /// <returns>Devuelve true si se ha actualizado el registro, de lo contrario devuelve false</returns>
        public bool editar(Asistencia entidadanterior, Asistencia entidadmodificada)
        {
            try
            {
                string sql = "UPDATE asistencias SET fecha_entrada='"+ entidadmodificada.fecha_entrada.ToString("MM/dd/yyyy") +"', " +
                    "hora_entrada='"+ entidadmodificada.hora_entrada.ToString("HH:mm") +"', " +
                    "fecha_salida='"+ entidadmodificada.fecha_salida.ToString("MM/dd/yyyy") +"', " +
                    "hora_salida='"+ entidadmodificada.hora_salida.ToString("HH:mm") +"' WHERE " +
                    "id_emp="+ entidadanterior.id_emp +" AND " +
                    "estado='Cerrado' AND fecha_entrada='"+ entidadanterior.fecha_entrada.ToString("MM/dd/yyyy") +"'";
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

        public bool eliminar(Asistencia entidad)
        {
            try
            {
                string sql = "DELETE FROM asistencias WHERE id=" + entidad.id;
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

        public IEnumerable<Asistencia> query(Expression<Func<Asistencia, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public Asistencia BuscarPorId(string id)
        {
            try
            {
                Asistencia dato = new Asistencia();
                db.Conectar();
                //dato = (Tipos_Produccion)db.Consulta("SELECT * FROM tipos_produccion WHERE id=" + Convert.ToInt32(id));
                SqlDataReader r = (SqlDataReader)db.Consulta("SELECT * FROM asistencias WHERE id=" + Convert.ToInt32(id));
                while (r.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(r[0].ToString());
                        dato.id_emp = Convert.ToInt32(r[1].ToString());
                        dato.fecha_entrada = Convert.ToDateTime(r[2].ToString());
                        dato.hora_entrada = Convert.ToDateTime(r[3].ToString());
                        dato.fecha_salida = Convert.ToDateTime(r[4].ToString());
                        dato.hora_salida = Convert.ToDateTime(r[5].ToString());
                        dato.estado = r[6].ToString();
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

        public bool validarAsistencia(Empleado emp)
        {
            db.Conectar();
            string sql = string.Format("select * from asistencias where id_emp=" + emp.id + " and estado= 'Abierto'");
            SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
            if (dr != null)
            {
                if (!dr.HasRows)
                {
                    db.Desconectar();
                    return true;
                }
                else
                {
                    db.Desconectar();
                    return false;
                }
            }
            else
            {
                db.Desconectar();
                return true;
            }
        }

        public bool registrar_asistencia(Empleado entidad, string tipo)
        {
            try
            {
                if (tipo == "Entrada")
                {
                    if (validarAsistencia(entidad))
                    {
                        string sql = "INSERT INTO asistencias (id_emp, fecha_entrada, hora_entrada, estado) VALUES (" + entidad.id + ", '" +
                            "" + DateTime.Now.ToString("MM/dd/yyyy") + "" +
                            "', '" + DateTime.Now.ToString("HH:mm") + "', 'Abierto')";
                        SqlCommand cmd = new SqlCommand(sql, db.conn);
                        db.Conectar();
                        db.Comando(cmd);
                        db.Desconectar();
                        Error = "";
                        return true;
                    } else
                    {
                        return false;
                    }
                }else if (tipo == "Salida")
                {
                    if (!validarAsistencia(entidad))
                    {
                        string sql = "UPDATE asistencias set fecha_salida='" + DateTime.Now.ToString("MM/dd/yyyy") +
                            "', hora_salida='" + DateTime.Now.ToString("HH:mm") + "', estado='Cerrado' WHERE id_emp=" +
                            entidad.id + " and estado='Abierto'";
                        SqlCommand cmd = new SqlCommand(sql, db.conn);
                        db.Conectar();
                        db.Comando(cmd);
                        db.Desconectar();
                        Error = "";
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }else
                {
                    return false;
                }
            } catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }


        }

        public bool editar_todo(Asistencia entidad)
        {
            SqlCommand cmd = new SqlCommand("UPDATE asistencias set " +
                "fecha_entrada= '" + entidad.fecha_entrada.ToString("MM/dd/yyyy") + "', " +
                "hora_entrada= '" + entidad.hora_entrada.ToString("HH:mm") + "', " +
                "hora_salida= '" + entidad.hora_salida.ToString("HH:mm") + "', " +
                "fecha_salida= '" + entidad.fecha_salida.ToString("MM/dd/yyyy") + "' WHERE " +
                "id= " + entidad.id, db.conn);
            db.Conectar();
            db.Comando(cmd);
            db.Desconectar();
            Error = "";
            return true;
        }

        public IEnumerable<Consulta_asistencia> buscarPorfecha(DateTime fechainicio, DateTime fechafinal)
        {
            return consultar_asistencias.Where(p => p.fecha_entrada >= fechainicio & p.fecha_salida <= fechafinal);
        }

        public IEnumerable<Asistencia> buscarfecha(DateTime fecha)
        {
            return leer.Where(p => p.fecha_entrada == fecha);
        }
    }
}
