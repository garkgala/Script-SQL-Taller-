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
    public class RepositorioProductos : IManejadorProducto
    {
        Basededatos db;
        public string Error { get; private set; }
        private AbstractValidator<Producto> validador;
        public RepositorioProductos(AbstractValidator<Producto> v)
        {
            this.validador = v;
            db = new Basededatos();
        }
        public IEnumerable<Producto> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from productos");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Producto> datos = new List<Producto>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Producto dato = new Producto
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                descripcion = dr[1].ToString(),
                                tipo_producto = dr[2].ToString(),
                                cantidad = Convert.ToInt32(dr[3].ToString()),
                                precio = Convert.ToDouble(dr[4].ToString())
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

        public Producto BuscarPorId(string id)
        {
            try
            {
                Producto dato = new Producto();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM productos WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.descripcion = dr[1].ToString();
                        dato.tipo_producto = dr[2].ToString();
                        dato.cantidad = Convert.ToInt32(dr[3].ToString());
                        dato.precio = Convert.ToDouble(dr[4].ToString());
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

        public bool crear(Producto entidad)
        {
            try
            {
                string sql = "INSERT INTO productos VALUES('"+ entidad.descripcion+"', " +
                    "'"+entidad.tipo_producto+"',"+entidad.cantidad+","+entidad.precio+" )";
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

        public bool editar(Producto entidadanterior, Producto entidadmodificada)
        {
            try
            {
                string sql = "UPDATE productos SET descripcion='" + entidadmodificada.descripcion + "', " +
                    "tipo_producto='" + entidadmodificada.tipo_producto + "', " +
                    "cantidad=" + entidadmodificada.cantidad + ", " +
                    "precio=" + entidadmodificada.precio + " WHERE id=" + entidadanterior.id;
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

        public bool eliminar(Producto entidad)
        {
            try
            {
                string sql = "DELETE FROM productos WHERE id=" + entidad.id;
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

        public IEnumerable<Producto> query(Expression<Func<Producto, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public IEnumerable<Producto> BuscarProductoPorNombre(string criterio)
        {
            return query(pr => pr.descripcion.ToLower().Contains(criterio.ToLower()));
        }

        public Producto BuscarPorNombreExacto(string criterio)
        {
            return query(prod => prod.descripcion == criterio).SingleOrDefault();
        }
    }
}
