using Comun.Interfaces;
using Comun.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;
using System.Reflection;

namespace Datos
{
    /// <summary>
    /// Proporciona datos basicos de acceso a una tabla de base de datos
    /// </summary>
    /// <typeparam name="T">Tipo de entidad clase (a la que se refiere a una tabla de la base de datos)</typeparam>
    public class RepositorioGenerico<T> where T : Base
    {
        Idb db;
        private bool idEsAutonumerico;
        private AbstractValidator<T> validador;
        public RepositorioGenerico(AbstractValidator<T> validador, bool idautonumerico = true)
        {
            this.validador = validador;
            this.idEsAutonumerico = idautonumerico;
            db = new Basededatos();
        }
        public string Error { get; private set; }
        public IEnumerable<T> leer
        {
            get
            {
                try
                {
                    //Realizara la consulta a la tabla que se pasa en la entidad (Por eso las entidades tienen que tener el nombre exacto como en la base de datos)
                    //T es la entidad por eso es un repositorio generico.
                    string sql = string.Format("SELECT * FROM {0};", typeof(T).Name);
                    //El metodo consulta de mi clase DbMysql devuelve un object pero no se cual objeto devuelve por eso se castea (obliga) a que
                    //devuelva un objeto MySqlDataReader.
                    SqlDataReader r = (SqlDataReader)db.Consulta(sql);
                    //Se crea una lista de campos de T (la entidad).
                    List<T> datos = new List<T>();
                    //Campos obtiene las propiedades de mi entidad (todos los campos) (si se pasara cargo como entidad, devolveria IdCargo y Cargos).
                    var campos = typeof(T).GetProperties();
                    T dato;
                    //se crea una variable de tipo Type.
                    Type Ttypo = typeof(T);
                    if (r != null)
                    {
                        while (r.Read())
                        {
                            //Como no se que tipo es utilizamos Activator de fluent, que lo que hace es crear una instancia especifica de T y aun asi
                            //La obligo a que me devuelva un dato de tipo T al colocarle (T).
                            dato = (T)Activator.CreateInstance(typeof(T));
                            for (int i = 0; i < campos.Length; i++)
                            {
                                //Reflecion (esta en el using) me permite obtener propiedades de mis objetos dinamicamente.
                                //Extre la propiedad de cada campo
                                PropertyInfo prop = Ttypo.GetProperty(campos[i].Name);
                                //Le asigno el valor en este caso seria asi: setvalue(que archivo, cual valor).
                                prop.SetValue(dato, r[i]);
                            }
                            //Se agrega el dato a la lista de datos.
                            datos.Add(dato);
                        }
                        //Cerramos el DataReader.
                        r.Close();
                    }
                    //Asignamos el error en vacío.
                    Error = "";
                    //Devolvemos la lista de los datos guardados.
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
