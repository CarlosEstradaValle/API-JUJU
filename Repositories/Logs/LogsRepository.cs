
using API.Core.Context;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Composition;
using System.Data;
using static API.Models.Reply;
using API.Models;
using API.Interfaces.Logs;
using Microsoft.Data.SqlClient;
using API.Context;
using API.Logic.Helpers;

namespace API.Repositories.Logs
{
    public class LogsRepository : ILogs
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext conn;
        ReplyLogin r = new();
        public ApplicationDbContext conexion = new ApplicationDbContext();
        ResultQuery resultadoQuery = new ResultQuery();

        public LogsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new ApplicationDbContext();
        }

        #region Obtener la lista de logs
        public async Task<ReplyLogin> GetLogs()
        {
            NpgsqlConnection npgsqlConnection1 = conn.ConnectBD(_configuration);
            try
            {
                using (SqlConnection sqlcn = new SqlConnection(npgsqlConnection1.ConnectionString))
                {
                    await sqlcn.OpenAsync();
                    string query = @$"SELECT Id, Message, MessageTemplate, Level, TimeStamp, Exception, Properties FROM [dbo].[Logs]";

                    SqlCommand cmdT_Pt = new SqlCommand(query, sqlcn);
                    SqlDataReader sqldr = await cmdT_Pt.ExecuteReaderAsync();
                    List<LogsModel> listaLogs = new List<LogsModel>();
                    if (sqldr.HasRows)
                    {
                        while (await sqldr.ReadAsync())
                        {
                            LogsModel LogsModel = new LogsModel();
                            LogsModel.Id = Convert.ToInt32(sqldr["Id"]);
                            LogsModel.Message = sqldr["Message"].ToString();
                            LogsModel.MessageTemplate = sqldr["MessageTemplate"].ToString();
                            LogsModel.Level = sqldr["Level"].ToString();
                            LogsModel.TimeStamp = sqldr["TimeStamp"] != DBNull.Value ? sqldr["TimeStamp"].ToString() : "";
                            LogsModel.Exception = sqldr["Exception"].ToString();
                            LogsModel.Properties = sqldr["Properties"].ToString();
                            listaLogs.Add(LogsModel);
                        }
                    }
                    await sqldr.CloseAsync();

                    r.Data = listaLogs;
                    r.Flag = true;
                    r.Message = "successfully";
                    r.Status = 200;
                    return r;
                }
            }
            catch (Exception ex)
            {
                r.Flag = false;
                r.Status = 500;
                r.Message = "Error al obtener los Logs  - " + ex.Message;
                return r;
            }

        }
        #endregion
    }
}
