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
    public class RepositorioEmpleados : IManejadorEmpleados
    {
        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Empleado> validador;
        public RepositorioEmpleados(AbstractValidator<Empleado> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Empleado> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from empleados");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Empleado> datos = new List<Empleado>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Empleado dato = new Empleado
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                dni = dr[1].ToString(),
                                nombre_emp = dr[2].ToString(),
                                direccion_emp = dr[3].ToString(),
                                telefono_emp = dr[4].ToString(),
                                fecha_ingreso = Convert.ToDateTime(dr[5].ToString()),
                                cargo = dr[6].ToString(),
                                tipo_cargo = dr[7].ToString(),
                                tipo_pago = dr[8].ToString(),
                                sueldo = Convert.ToDouble(dr[9].ToString())
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

        public IEnumerable<Empleado> traerNombresEmpleados
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select nombre_emp from empleados");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Empleado> datos = new List<Empleado>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Empleado dato = new Empleado
                            {
                                nombre_emp = dr[0].ToString()
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
        /// <summary>
        /// Buscar los registros en la tabla empleado en base al dni proporcionado
        /// </summary>
        /// <param name="dni">Dni del empleado a buscar</param>
        /// <returns>Devuelve el registro Empleado que coincide con la busqueda proporcionada</returns>
        public Empleado BuscarPorId(string dni)
        {
            try
            {
                Empleado dato = new Empleado();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM empleados WHERE dni=" + dni);
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.dni = dr[1].ToString();
                        dato.nombre_emp = dr[2].ToString();
                        dato.direccion_emp = dr[3].ToString();
                        dato.telefono_emp = dr[4].ToString();
                        dato.fecha_ingreso = Convert.ToDateTime(dr[5].ToString());
                        dato.cargo = dr[6].ToString();
                        dato.tipo_cargo = dr[7].ToString();
                        dato.tipo_pago = dr[8].ToString();
                        dato.sueldo = Convert.ToDouble(dr[9].ToString());
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

        public bool crear(Empleado entidad)
        {
            try
            {
                string sql = "INSERT INTO empleados VALUES(" +
                    "'" + entidad.dni + "', '" + entidad.nombre_emp + "', '" + entidad.direccion_emp + "', '" + entidad.telefono_emp + "', '" +
                    "" + entidad.fecha_ingreso.ToString("MM/dd/yyyy") + "', '" + entidad.cargo + "', '" + entidad.tipo_cargo + "', '" +
                    "" + entidad.tipo_pago + "', " + entidad.sueldo + ")";
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

        public bool editar(Empleado entidadanterior, Empleado entidadmodificada)
        {
            try
            {
                string sql = "UPDATE empleados SET nombre_emp='" + entidadmodificada.nombre_emp + "', dni='" +
                    "" + entidadmodificada.dni +  "', direccion_emp='" +
                    "" + entidadmodificada.direccion_emp + "', telefono_emp='" + entidadmodificada.telefono_emp + "', " +
                    "fecha_ingreso='" + entidadmodificada.fecha_ingreso.ToString("MM/dd/yyyy") + "', cargo='" + entidadmodificada.cargo + "', " +
                    "tipo_cargo='" + entidadmodificada.tipo_cargo + "', tipo_pago='" + entidadmodificada.tipo_pago + "', " +
                    "sueldo=" + entidadmodificada.sueldo + " WHERE id=" + entidadmodificada.id;
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

        public bool eliminar(Empleado entidad)
        {
            try
            {
                string sql = "DELETE FROM empleados WHERE id=" + entidad.id;
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

        public IEnumerable<Empleado> query(Expression<Func<Empleado, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public IEnumerable<Empleado> BuscarPorNombre(string nombre)
        {
            return leer.Where(e => e.nombre_emp.ToLower().Contains(nombre.ToLower()));
        }
    }
}
