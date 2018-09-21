using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Indicadores.Library.Model.DB
{
    public class ConexionProcAlmacenados
    {

        MySqlConnection con;
        MySqlCommand command;

        private static ConexionProcAlmacenados instance = null;
        private string cadConexion;

        public static ConexionProcAlmacenados GetInstance()
        {
            if (instance == null)
                return new ConexionProcAlmacenados();

            return instance;
        }

        private ConexionProcAlmacenados()
        {
            cadConexion = ConfigurationManager.ConnectionStrings["mhealthdiabetesdb"].ToString();
        }

        public Resultado ObtenerDatosIndicadores(String nombreProcedimiento, List<Parametros> parametros)
        {
            Resultado objResultado = null;

            try
            {
                using (con = new MySqlConnection(cadConexion))
                {
                    command = new MySqlCommand(nombreProcedimiento, con);
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parametro in parametros)
                    {
                        command.Parameters.Add(new MySqlParameter(parametro.Nombre, parametro.Valor)).Direction = ParameterDirection.Input;
                    }

                    con.Open();

                    MySqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

                    if (dr.Read())
                    {
                        objResultado = new Resultado();
                        objResultado.Numerador = Convert.ToDouble(dr["numerador"]);
                        objResultado.Denominador = Convert.ToDouble(dr["denominador"]);
                        objResultado.ResultadoIndicador = Convert.ToDouble(dr["resultado"]);
                    }

                    dr.Close();
                }

                return objResultado;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        public ResultadoIndicador ObtenerDatosIndicadoresTotales(String nombreProcedimiento, List<Parametros> parametros)
        {
            ResultadoIndicador objResultado = null;

            try
            {
                using (con = new MySqlConnection(cadConexion))
                {

                    command = new MySqlCommand(nombreProcedimiento, con);
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parametro in parametros)
                    {
                        command.Parameters.Add(new MySqlParameter(parametro.Nombre, parametro.Valor)).Direction = ParameterDirection.Input;
                    }

                    con.Open();

                    MySqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

                    if (dr.Read())
                    {
                        objResultado = new ResultadoIndicador();
                        objResultado.NoMedidos = Convert.ToInt32(dr["noMedidos"]);
                        objResultado.PacientesControlados = Convert.ToInt32(dr["pacientesControlados"]);
                        objResultado.PacientesEstadios = Convert.ToInt32(dr["pacientesEstadios"]);
                        objResultado.PacientesEstudiados = Convert.ToInt32(dr["pacientesEstudiados"]);
                        objResultado.VigentesControlados = Convert.ToInt32(dr["vigentesControlados"]);
                        objResultado.VigentesDescontrolados = Convert.ToInt32(dr["vigentesDescontrolados"]);
                    }

                    dr.Close();
                }

                return objResultado;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        public class Parametros
        {
            public String Nombre { get; set; }

            public Object Valor { get; set; }

            public static Parametros Build(String nombre, Object value)
            {
                return new Parametros
                {
                    Nombre = nombre,
                    Valor = value
                };
            }
        }
    }
}
