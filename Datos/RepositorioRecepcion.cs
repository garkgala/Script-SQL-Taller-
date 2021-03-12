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
    public class RepositorioRecepcion : IManejadorRecepcion
    {
        Basededatos db;
        AbstractValidator<Recepcion> validador;
        public string Error { get; private set; }
        public RepositorioRecepcion(AbstractValidator<Recepcion> v)
        {
            this.validador = v;
            db = new Basededatos();
        }

        public IEnumerable<Recepcion> leer
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from recepcion");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<Recepcion> datos = new List<Recepcion>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            Recepcion dato = new Recepcion
                            {
                                id = Convert.ToInt32(dr[0].ToString()),
                                fecha = Convert.ToDateTime(dr[1].ToString()),
                                id_proveedor = Convert.ToInt32(dr[2].ToString()),
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

        public IEnumerable<consulta_compra> consultarCompras
        {
            get
            {
                try
                {
                    db.Conectar();
                    string sql = string.Format("select * from consulta_compras");
                    SqlDataReader dr = (SqlDataReader)db.Consulta(sql);
                    List<consulta_compra> datos = new List<consulta_compra>();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            consulta_compra dato = new consulta_compra
                            {
                                fecha = Convert.ToDateTime(dr[0].ToString()),
                                nombre_prov = dr[1].ToString(),
                                descripcion = dr[2].ToString(),
                                cantidad = Convert.ToInt32(dr[3].ToString()),
                                precio = Convert.ToDouble(dr[4].ToString()),
                                total = Convert.ToDouble(dr[5].ToString())
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
        public Recepcion BuscarPorId(string id)
        {
            try
            {
                Recepcion dato = new Recepcion();
                db.Conectar();
                SqlDataReader dr = (SqlDataReader)db.Consulta("SELECT * FROM recepcion WHERE id=" + Convert.ToInt32(id));
                while (dr.Read())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dato.id = Convert.ToInt32(dr[0].ToString());
                        dato.fecha = Convert.ToDateTime(dr[1].ToString());
                        dato.id_proveedor = Convert.ToInt32(dr[2].ToString());
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

        public bool crear(Recepcion entidad)
        {
            try
            {
                string sql = "INSERT INTO recepcion VALUES ('"+entidad.fecha.ToString("MM/dd/yyyy")+"', " +
                    ""+entidad.id_proveedor+", "+entidad.id_prod+", "+entidad.cantidad+")";
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

        public bool editar(Recepcion entidadanterior, Recepcion entidadmodificada)
        {
            throw new NotImplementedException();
        }

        public bool eliminar(Recepcion entidad)
        {
            try
            {
                string sql = "DELETE FROM recepcion WHERE id=" + entidad.id;
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

        public IEnumerable<Recepcion> query(Expression<Func<Recepcion, bool>> predicado)
        {
            return leer.Where(predicado.Compile());
        }

        public IEnumerable<consulta_compra> ComprasPorFechas(DateTime inicio, DateTime fin)
        {
            return consultarCompras.Where(p => p.fecha >= inicio & p.fecha <= fin);
        }
    }
}
