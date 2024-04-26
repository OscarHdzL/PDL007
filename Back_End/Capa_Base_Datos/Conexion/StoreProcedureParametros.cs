using MySqlConnector;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using Npgsql;
using System.Text;

namespace Conexion
{
    /// <summary>
    /// 
    /// </summary>
    public static class StoreProcedureParametros
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListParam"></param>
        /// <param name="Query"></param>
        /// <returns></returns>
        public static EntidadModelado<MySqlParameter> ParametrosMySQL(List<EntidadParametro> ListParam, string Query)
        {
            StringBuilder bld = new StringBuilder();

            EntidadModelado<MySqlParameter> ParametrosmySQL = new EntidadModelado<MySqlParameter>();
            bld.Append("CALL " + Query + "(");
            int count = 0;
            foreach (var item in ListParam)
            {
                MySqlParameter Parametro = new MySqlParameter();
                if (item.Valor.Equals("NULL"))
                {
                    if (count == 0)
                    {
                        bld.Append("NULL");
                    }
                    else
                    {
                        bld.Append(",NULL");
                    }
                }
                else
                {
                    if (count == 0)
                    {
                        bld.Append("" + "@" + item.Nombre);
                    }
                    else
                    {
                        bld.Append("," + "@" + item.Nombre);
                    }
                    ParametrosmySQL.Query = bld.ToString();
                    Parametro.ParameterName = item.Nombre;
                    Parametro.MySqlDbType = TipoDatoMySQL(item.Tipo);
                    Parametro.Value = item.Valor;
                    ParametrosmySQL.ListaParametros.Add(Parametro);
                }
                count++;
            }
            ParametrosmySQL.Query += ")";

            return ParametrosmySQL;
        }
        public static EntidadModelado<NpgsqlParameter> ParametrosPostgreSQL(List<EntidadParametro> ListParam, string Query = "", string tipo = "CALL")
        {
            StringBuilder bld = new StringBuilder();

            EntidadModelado<NpgsqlParameter> ParametrosmySQL = new EntidadModelado<NpgsqlParameter>();
            bld.Append($"{tipo} " + Query + "(");
            int count = 0;
            if (ListParam != null)
                foreach (var item in ListParam)
                {
                    NpgsqlParameter Parametro = new NpgsqlParameter();
                    if (item.Valor?.ToString() == "NULL")
                    {
                        if (count == 0)
                        {
                            bld.Append("NULL");
                        }
                        else
                        {
                            bld.Append(",NULL");
                        }
                        ParametrosmySQL.Query = bld.ToString();                      
                    }
                    //si solo tiene un parametro y es null
                    //else
                    //if (ListParam.Count == 1)
                    //{
                    //    ParametrosmySQL.Query = bld.ToString();
                    //    Parametro.ParameterName = item.Nombre;
                    //    if (item.Tipo == "Boolean" && item.Valor == null)
                    //    {
                    //        item.Valor = DBNull.Value;
                    //    }
                    //    Parametro.NpgsqlDbType = TipoDatosPostgre(item.Tipo);
                    //    Parametro.Value = item.Valor;
                    //    ParametrosmySQL.ListaParametros.Add(Parametro);
                    //}
                    else
                    {
                        if (count == 0)
                        {
                            bld.Append("" + "@" + item.Nombre);
                        }
                        else
                        {
                            bld.Append("," + "@" + item.Nombre);
                        }
                        ParametrosmySQL.Query = bld.ToString();
                        Parametro.ParameterName = item.Nombre;
                        Parametro.NpgsqlDbType = TipoDatosPostgre(item.Tipo);
                        Parametro.Value = item.Valor ?? DBNull.Value;
                        ParametrosmySQL.ListaParametros.Add(Parametro);
                    }

                    count++;
                }
            else
                ParametrosmySQL.Query += $"{tipo} " + Query + "(";
            ParametrosmySQL.Query += ")";

            return ParametrosmySQL;
        }

        /// <summary>
        /// TipoDatoMySQL
        /// </summary>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static MySqlDbType TipoDatoMySQL(String Tipo)
        {

            switch (Tipo)
            {
                case "String":
                    return MySqlDbType.VarChar;
                case "Int":
                    return MySqlDbType.Int32;
                case "Boolean":
                    return MySqlDbType.Bit;
                case "Decimal":
                    return MySqlDbType.Decimal;
                case "Float":
                    return MySqlDbType.Float;
                case "DateTime":
                    return MySqlDbType.DateTime;
                case "JSON":
                    return MySqlDbType.JSON;
                case "Time":
                    return MySqlDbType.Time;
                default:
                    return MySqlDbType.VarChar;

            }
        }
        /// <summary>
        /// TipoDatosPostgre
        /// </summary>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static NpgsqlDbType TipoDatosPostgre(String Tipo)
        {
            switch (Tipo)
            {
                case "String":
                    return NpgsqlDbType.Varchar;
                case "Int":
                    return NpgsqlDbType.Integer;
                case "Boolean":
                    return NpgsqlDbType.Boolean;
                case "Double":
                    return NpgsqlDbType.Double;
                case "Date":
                    return NpgsqlDbType.Date;
                case "Real":
                    return NpgsqlDbType.Real;
                case "DateTime":
                    return NpgsqlDbType.Timestamp;
                case "JSON":
                    return NpgsqlDbType.Json;
                case "Time":
                    return NpgsqlDbType.Time;
                case "Text":
                    return NpgsqlDbType.Text;
                case "Bit":
                    return NpgsqlDbType.Bit;
                default:
                    return NpgsqlDbType.Varchar;

            }
        }

    }

    /// <summary>
    /// EntidadModelado
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntidadModelado<T>
    {

        public EntidadModelado()
        {

            ListaParametros = new List<T>();
        }
        public string Query { get; set; }
        public List<T> ListaParametros { get; set; }
    }
    /// <summary>
    /// EntidadParametro
    /// </summary>
    public class EntidadParametro
    {

        public string Nombre { get; set; }
        public object Valor { get; set; }
        public string Tipo { get; set; }
    }

}
