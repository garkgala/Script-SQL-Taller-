using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Comun.Interfaces;

namespace Datos
{
    public class Basededatos : Idb
    {
        public string Error { get; private set; }
        public SqlConnection conn;
        public Basededatos()
        {
            string conexionstring;
            conexionstring = System.IO.File.ReadAllText(@"C:\conexion.txt");
            //El @ establece que tome el string tal como se escribe por el motivo de la \
            conn = new SqlConnection(conexionstring);
            //conn = new SqlConnection($@"Data Source=(LocalDB)\SQLExpress;AttachDbFilename=|DataDirectory|taller.mdf;Integrated Security=True");
            //conn = new SqlConnection($@"Data Source=LEONEL-PC\SQLEXPRESS;Initial Catalog=taller;Integrated Security=SSPI;");
            //Server =.\SQLExpress; AttachDbFilename =| DataDirectory | mydbfile.mdf; Database = dbname; Trusted_Connection = Yes;
            //conn = new SqlConnection($"Data Source={server};Initial Catalog={db};User Id={user}; Password={clave};");
        }
        public bool Conectar()
        {
                try
            {
                conn.Open();
                Error = "";
                return true;
            }
            catch(SqlException ex)
            {
                Error = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Ejecuta un comando sql
        /// </summary>
        /// <param name="comando">Comando sql ya definido (con sus parametros si es SP)</param>
        /// <returns>Devuelve true si el comando se ejecuto correctamente, de otro modo devuelve false.</returns>
        public bool Comando(SqlCommand comando)
        {
            try
            {
                comando.ExecuteNonQuery();
                Error = "";
                return true;
            }
            catch(Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
        public object Consulta(string consulta)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(consulta, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                Error = "";
                return dr;
            }
            catch(Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }
        public bool Desconectar()
        {
            try
            {
                conn.Close();
                Error = "";
                return true;
            }
            catch(SqlException ex)
            {
                Error = ex.Message;
                return false;
            }
        }
    }
}
