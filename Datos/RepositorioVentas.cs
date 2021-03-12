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
    public class RepositorioVentas : IManejadorVentas
    {
        Basededatos db;
        AbstractValidator<Venta> validador;
        public string Error { get; private set; }
        public RepositorioVentas(AbstractValidator<Venta> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Venta> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from ventas");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Venta> datos = new List<Venta>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Venta dato = new Venta
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                n_factura = dr[1].ToString(),
                                fecha_venta = Convert.ToDateTime(dr[2].ToString()),
                                id_cli = Convert.ToInt64(dr[3].ToString()),
                                id_prod = Convert.ToInt64(dr[4].ToString()),
                                cantidad = Convert.ToInt32(dr[5].ToString()),
                                precio_venta = Convert.ToDouble(dr[6].ToString())
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

        public IEnumerable<ventasdetalle> VentasEnDetalle
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from ventas_totales");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<ventasdetalle> datos = new List<ventasdetalle>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            ventasdetalle dato = new ventasdetalle
                            {
                                fecha_venta = Convert.ToDateTime(dr[0].ToString()),
                                nombre_cli = dr[1].ToString(),
                                descripcion = dr[2].ToString(),
                                cantidad = Convert.ToInt32(dr[3].ToString()),
                                precio_venta = Convert.ToDouble(dr[4].ToString()),
                                Total = Convert.ToDouble(dr[5].ToString())
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

        public Venta BuscarPorId(string id)
        {
            try
            {
                Venta dato = new Venta();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM ventas WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.n_factura = dr[1].ToString();
                        dato.fecha_venta = Convert.ToDateTime(dr[2].ToString());
                        dato.id_cli = Convert.ToInt64(dr[3].ToString());
                        dato.id_prod = Convert.ToInt64(dr[4].ToString());
                        dato.cantidad = Convert.ToInt32(dr[5].ToString());
                        dato.precio_venta = Convert.ToDouble(dr[6].ToString());
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

        public bool crear(Venta entidad)
        {
            try
            {
                string sql = "INSERT INTO ventas VALUES (" +
                    "'"+entidad.n_factura+"', " +
                    "'"+entidad.fecha_venta.ToString("MM/dd/yyyy")+"', " +
                    ""+entidad.id_cli+", " +
                    ""+entidad.id_prod+", " +
                    ""+entidad.cantidad+", " +
                    ""+entidad.precio_venta+")";
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

        public bool editar(Venta entidadanterior, Venta entidadmodificada)
        {
            try
            {
                string sql = "UDATE ventas SET " +
                    "n_factura='" + entidadmodificada.n_factura + "', " +
                    "fecha_venta='" + entidadmodificada.fecha_venta + "', " +
                    "id_cli=" + entidadmodificada.id_cli + ", " +
                    "id_prod=" + entidadmodificada.id_prod + ", " +
                    "cantidad=" + entidadmodificada.cantidad + ", " +
                    "precio_venta=" + entidadmodificada.precio_venta + " WHERE id=" + entidadanterior.id;
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

        public bool eliminar(Venta entidad)
        {
            try
            {
                string sql = "DELETE FROM ventas WHERE id=" + entidad.id;
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

        public IEnumerable<Venta> query(Expression<Func<Venta, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public IEnumerable<Venta> VentasDeClientteEnIntervalo(int id_cli, DateTime inicio, DateTime fin)
        {
            return leer.Where(ve => ve.id_cli == id_cli && ve.fecha_venta >= inicio && ve.fecha_venta <= fin).ToList();
        }

        public IEnumerable<ventasdetalle> VentasEnIntervalo(DateTime inicio, DateTime fin)
        {
            return VentasEnDetalle.Where(ve => ve.fecha_venta >= inicio && ve.fecha_venta <= fin).ToList();
        }

        public bool validar_factura(string n_factura)
        {
            Venta venta = query(p => p.n_factura == n_factura).SingleOrDefault();
            if (venta != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
