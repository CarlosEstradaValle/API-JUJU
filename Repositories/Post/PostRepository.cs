using API.Core.Context;
using API.Interfaces.Post;
using API.Models;
using Microsoft.Data.SqlClient;
using Npgsql;
using static API.Models.Reply;

namespace API.Repositories.Post
{
    public class PostRepository : IPost
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext conn;
        ReplyLogin r = new();

        public PostRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new ApplicationDbContext();
        }

        #region Obtener la lista de Post
        public async Task<ReplyLogin> GetPost()
        {
            NpgsqlConnection npgsqlConnection1 = conn.ConnectBD(_configuration);
            try
            {
                using (SqlConnection sqlcn = new SqlConnection(npgsqlConnection1.ConnectionString))
                {
                    await sqlcn.OpenAsync();
                    string query = @$"SELECT * FROM [dbo].[Post]";

                    SqlCommand cmdT_Pt = new SqlCommand(query, sqlcn);
                    SqlDataReader sqldr = await cmdT_Pt.ExecuteReaderAsync();
                    List<PostModel> listaPots = new List<PostModel>();
                    if (sqldr.HasRows)
                    {
                        while (await sqldr.ReadAsync())
                        {
                            PostModel cd = new PostModel();
                            cd.PostId = Convert.ToInt32(sqldr["PostId"]);
                            cd.Title = sqldr["Title"].ToString();
                            cd.Body = sqldr["Body"].ToString();
                            cd.Type = Convert.ToInt32(sqldr["Type"]);
                            cd.Category = sqldr["Category"].ToString();
                            cd.CustomerId = Convert.ToInt32(sqldr["CustomerId"]);
                            listaPots.Add(cd);
                        }
                    }
                    await sqldr.CloseAsync();

                    r.Data = listaPots;
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
                r.Message = "Error al obtener los Post  - " + ex.Message;
                return r;
            }



        }
        #endregion

        #region Método asincrono para crear los pots
        public async Task<ReplyLogin> CreatePots(PostModel data)
        {
            NpgsqlConnection npgsqlConnection1 = conn.ConnectBD(_configuration);
            try
            {
                using (SqlConnection sqlcn = new SqlConnection(npgsqlConnection1.ConnectionString))
                {
                    await sqlcn.OpenAsync();
                    string query = @$"SELECT * FROM [dbo].[Post] WHERE CustomerId = '{data.CustomerId}'";

                    SqlCommand cmdT_Pt = new SqlCommand(query, sqlcn);
                    SqlDataReader sqldr = await cmdT_Pt.ExecuteReaderAsync();
                    string body = "";
                    if (sqldr.HasRows)
                    {
                        if (data.Body.Length > 20) { body = data.Body.Substring(97) + "...."; } else { body = data.Body; }

                        switch (data.Type)
                        {
                            case 1:
                                data.Category = "Farándula";
                                break;
                            case 2:
                                data.Category = "Política";
                                break;
                            case 3:
                                data.Category = "Futbol";
                                break;
                            default:
                                break;
                        }
                        query = @$"INSERT INTO [dbo].[Post]  (Title,Body,Type,Category,CustomerId) VALUES ('{data.Title}', '{body}', {data.Type} ,'{data.Category}', {data.CustomerId} ))";

                        cmdT_Pt = new SqlCommand(query, sqlcn);
                        sqldr = await cmdT_Pt.ExecuteReaderAsync();
                        if (sqldr.HasRows)
                        {
                            r.Data = null;
                            r.Flag = true;
                            r.Message = "El Post fue creado exitosamente";
                            r.Status = 200;
                            return r;
                        }


                        r.Data = null;
                        r.Flag = true;
                        r.Message = "El Post no se ha creado";
                        r.Status = 200;
                        return r;
                    }

                    await sqldr.CloseAsync();

                    r.Data = null;
                    r.Flag = true;
                    r.Message = "El usuario no existe";
                    r.Status = 200;
                    return r;
                }
            }
            catch (Exception ex)
            {
                r.Flag = false;
                r.Status = 500;
                r.Message = "Error al crear los Post  - " + ex.Message;
                return r;
            }
        }
        #endregion
    }
}
