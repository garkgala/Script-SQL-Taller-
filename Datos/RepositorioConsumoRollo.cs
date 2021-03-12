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
    public class RepositorioConsumoRollo : IManejadorConsumoRollos
    {
        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Consumo_rollo> validador;
        public RepositorioConsumoRollo(AbstractValidator<Consumo_rollo> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Consumo_rollo> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from consumo_rollos");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Consumo_rollo> datos = new List<Consumo_rollo>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Consumo_rollo dato = new Consumo_rollo
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                fecha = Convert.ToDateTime(dr[1].ToString()),
                                id_prod = Convert.ToInt64(dr[2].ToString()),
                                cantidad = Convert.ToInt64(dr[3].ToString())
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

        public IEnumerable<VistaConsumos> visualizarConsumos
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from visualizar_consumos");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<VistaConsumos> datos = new List<VistaConsumos>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            VistaConsumos dato = new VistaConsumos
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                fecha = Convert.ToDateTime(dr[1].ToString()).ToShortDateString(),
                                descripcion = dr[2].ToString(),
                                cantidad = Convert.ToInt32(dr[3].ToString())
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

        public Consumo_rollo BuscarPorId(string id)
        {
            try
            {
                Consumo_rollo dato = new Consumo_rollo();
                db.Conectar();
                SqlDataReader r = (SqlDataReader)db.Consulta("SELECT * FROM consumo_rollos WHERE id=" + Convert.ToInt32(id));
                while (r.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(r[0].ToString());
                        dato.fecha = Convert.ToDateTime(r[1].ToString());
                        dato.id_prod = Convert.ToInt32(r[2].ToString());
                        dato.cantidad = Convert.ToInt64(r[3].ToString());
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

        public bool crear(Consumo_rollo entidad)
        {
            try
            {
                string sql = "INSERT INTO consumo_rollo VALUES('" + entidad.fecha.ToString("MM/dd/yyyy") + "'," +
                    "" + entidad.id + " , " + entidad.cantidad + ")";
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

        public bool editar(Consumo_rollo entidadanterior, Consumo_rollo entidadmodificada)
        {
            throw new NotImplementedException();
        }

        public bool eliminar(Consumo_rollo entidad)
        {
            try
            {
                string sql = "DELETE FROM consumo_rollo WHERE id=" + entidad.id;
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

        public IEnumerable<Consumo_rollo> query(Expression<Func<Consumo_rollo, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public bool consumir_rollo(Producto producto, int cantidad)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO consumo_rollo (fecha, id_prod, cantidad) VALUES ('" + DateTime.Now.ToString("MM/dd/yyyy") + "', " + producto.id + ", " + cantidad + ")", db.conn);
            try
            {
                db.Conectar();
                db.Comando(cmd);
                db.Desconectar();
                Error = "";
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                return false;
            }
        }

        public IEnumerable<VistaConsumos> BuscarConsumoPorFecha(DateTime fecha)
        {
            return visualizarConsumos.Where(p => Convert.ToDateTime(p.fecha) == fecha);
        }
    }
}
