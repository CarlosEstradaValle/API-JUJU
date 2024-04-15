using API.Context;
using API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public static NpgsqlConnection objConexionP;
        private static string conecction = "BDConnection1";

        public NpgsqlConnection ConnectBD(IConfiguration configuration)
        {
            objConexionP = new NpgsqlConnection();
            objConexionP.ConnectionString = configuration.GetConnectionString("BDConnection1");
            return objConexionP;
        }

        public string ConnectBDCliente(IConfiguration configuration)
        {
            return configuration.GetConnectionString(conecction);
        }

        public static void SetConnection(string name)
        {
            conecction = name;
        }

        public SqlConnection ConnectBD(IConfiguration configuration, string name = "BDConnection1")
        {
            return new SqlConnection(configuration.GetConnectionString(name));
        }


        public ResultQuery RunQuery(string sqlStr, CommandType type, string conexionServer)
        {
            ResultQuery resultQuery = new ResultQuery();
            resultQuery.Table = new DataTable();
            resultQuery.Result = "";
            resultQuery.ResultMessage = "";

            bool persistenceResponse = false;
            int connectionAttempts = 0;
            string errorMessage = string.Empty;

            while (persistenceResponse == false)
            {
                NpgsqlConnection connection = new NpgsqlConnection(conexionServer);
                NpgsqlCommand command = new NpgsqlCommand();
                connectionAttempts += 1;

                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close(); connection.Open();
                    }
                    else
                    {
                        connection.Open();
                    }

                    command = connection.CreateCommand();
                    command.CommandType = type;
                    command.CommandText = sqlStr;

                    DataTable dt = new DataTable();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
                    da.Fill(dt);
                    connection.Close();

                    persistenceResponse = true;
                    resultQuery.Table = dt;
                }
                catch (Exception ex)
                {
                    //REGISTRAR LOG
                    errorMessage = ex.Message;
                    errorMessage = ex.Message;
                    persistenceResponse = false;
                    resultQuery.Result = "ERROR";
                    resultQuery.ResultMessage = errorMessage;
                }
                finally
                {
                    connection.Dispose();
                    connection.Close();
                }

                if (persistenceResponse == false && connectionAttempts == 5)
                {
                    //SALE DEL CICLO PARA DEVOLVER EL ERROR
                    persistenceResponse = true;
                }
            }

            return resultQuery;
        }

    }
}
