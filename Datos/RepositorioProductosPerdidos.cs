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
    public class RepositorioProductosPerdidos:IManejadorProdPerdido
    {
        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Producto_Perdido> validador;
        public RepositorioProductosPerdidos(AbstractValidator<Producto_Perdido> v)
        {
            this.validador = v;
            db = new Basededatos();
        }

        public IEnumerable<Producto_Perdido> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from productos_perdidos");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Producto_Perdido> datos = new List<Producto_Perdido>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Producto_Perdido dato = new Producto_Perdido
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                id_prod = Convert.ToInt32(dr[1].ToString()),
                                registrado_por = dr[2].ToString(),
                                fecha = Convert.ToDateTime(dr[3].ToString()),
                                cantidad = Convert.ToInt32(dr[4].ToString()),
                                observaciones = dr[5].ToString()
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

        public Producto_Perdido BuscarPorId(string id)
        {
            try
            {
                Producto_Perdido dato = new Producto_Perdido();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM productos_perdidos WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.id_prod = Convert.ToInt32(dr[1].ToString());
                        dato.registrado_por = dr[2].ToString();
                        dato.fecha = Convert.ToDateTime(dr[3].ToString());
                        dato.cantidad = Convert.ToInt32(dr[4].ToString());
                        dato.observaciones = dr[5].ToString();
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

        public bool crear(Producto_Perdido entidad)
        {
            try
            {
                string sql = "INSERT INTO productos_perdidos VALUES (" +
                    ""+entidad.id_prod+", '"+entidad.registrado_por+"', " +
                    "'"+entidad.fecha.ToString("MM/dd/yyy")+"', " +
                    ""+entidad.cantidad+", '"+entidad.observaciones+"')";
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

        public bool editar(Producto_Perdido entidadanterior, Producto_Perdido entidadmodificada)
        {
            //No se le implementara edicion a esta entidad, solo permitira agregar y eliminar
            throw new NotImplementedException();
        }

        public bool eliminar(Producto_Perdido entidad)
        {
            try
            {
                string sql = "DELETE FROM productos_perdidos WHERE id=" + entidad.id;
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

        public IEnumerable<Producto_Perdido> query(Expression<Func<Producto_Perdido, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public IEnumerable<VistaProductoPerdido> VisualizarPerdidosPorFecha(DateTime fechainicio, DateTime fechafin)
        {
            return VisualizarPerdidos.Where(p => Convert.ToDateTime(p.Fecha) >= fechainicio & Convert.ToDateTime(p.Fecha) <= fechafin);
        }

        public IEnumerable<VistaProductoPerdido> VisualizarPerdidos
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from visualizar_perdidos");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<VistaProductoPerdido> datos = new List<VistaProductoPerdido>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            VistaProductoPerdido dato = new VistaProductoPerdido
                            {
                                Descripcion = dr[0].ToString(),
                                Responsable = dr[1].ToString(),
                                Fecha =Convert.ToDateTime(dr[2].ToString()).ToShortDateString(),
                                Cantidad = Convert.ToInt16(dr[3].ToString()),
                                Observaciones = dr[4].ToString()
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
    }
}
