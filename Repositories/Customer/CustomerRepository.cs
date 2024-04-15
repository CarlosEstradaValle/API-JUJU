using API.Core.Context;
using API.Interfaces.Customer;
using API.Models;
using Microsoft.Data.SqlClient;
using Npgsql;
using static API.Models.Reply;

namespace API.Repositories.Customer
{
    public class CustomerRepository: ICustomer
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext conn;
        ReplyLogin r = new();

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new ApplicationDbContext();
        }

        #region Obtener la lista de Customer
        public async Task<ReplyLogin> GetCustomer()
        {
            NpgsqlConnection npgsqlConnection1 = conn.ConnectBD(_configuration);
            try
            {
                using (SqlConnection sqlcn = new SqlConnection(npgsqlConnection1.ConnectionString))
                {
                    await sqlcn.OpenAsync();
                    string query = @$"SELECT CustomerId, Name FROM [dbo].[Customer]";

                    SqlCommand cmdT_Pt = new SqlCommand(query, sqlcn);
                    SqlDataReader sqldr = await cmdT_Pt.ExecuteReaderAsync();
                    List<CustomerModel> listaCus = new List<CustomerModel>();
                    if (sqldr.HasRows)
                    {
                        while (await sqldr.ReadAsync())
                        {
                            CustomerModel cd = new CustomerModel();
                            cd.CustomerId = Convert.ToInt32(sqldr["CustomerId"]);
                            cd.Name = sqldr["Name"].ToString();
                            listaCus.Add(cd);
                        }
                    }
                    await sqldr.CloseAsync();

                    r.Data = listaCus;
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
                r.Message = "Error al obtener los Customer  - " + ex.Message;
                return r;
            }



        }
        #endregion

        #region Crear customer
        public async Task<ReplyLogin> CreateCustomer(CustomerModel data)
        {
            NpgsqlConnection npgsqlConnection1 = conn.ConnectBD(_configuration);
            try
            {
                using (SqlConnection sqlcn = new SqlConnection(npgsqlConnection1.ConnectionString))
                {
                    await sqlcn.OpenAsync();
                    string query = @$"SELECT * FROM [dbo].[Customer] WHERE NAME = '{data.Name}'";

                    SqlCommand cmdT_Pt = new SqlCommand(query, sqlcn);
                    SqlDataReader sqldr = await cmdT_Pt.ExecuteReaderAsync();
                    List<CustomerModel> listaCus = new List<CustomerModel>();
                    if (sqldr.HasRows)
                    {
                        while (await sqldr.ReadAsync())
                        {
                            CustomerModel cd = new CustomerModel();
                            cd.CustomerId = Convert.ToInt32(sqldr["CustomerId"]);
                            cd.Name = sqldr["Name"].ToString();
                            listaCus.Add(cd);
                        }
                        r.Data = listaCus;
                        r.Flag = true;
                        r.Message = "El usuario ya existe";
                        r.Status = 200;
                        return r;
                    }
                    else
                    {
                        query = @$"INSERT INTO [dbo].[Customer]  (Name) VALUES ('{usuario}')";

                        cmdT_Pt = new SqlCommand(query, sqlcn);
                        sqldr = await cmdT_Pt.ExecuteReaderAsync();
                        if (sqldr.HasRows)
                        {
                            r.Data = listaCus;
                            r.Flag = true;
                            r.Message = "Usuario creado exitosamente";
                            r.Status = 200;
                            return r;
                        }
                    }
                    await sqldr.CloseAsync();

                    r.Data = listaCus;
                    r.Flag = true;
                    r.Message = "No se pudo crear el usuario";
                    r.Status = 200;
                    return r;
                }
            }
            catch (Exception ex)
            {
                r.Flag = false;
                r.Status = 500;
                r.Message = "Error al crear los Customer  - " + ex.Message;
                return r;
            }
        }
        #endregion

        #region Actualizar customer
        public async Task<ReplyLogin> UpdateCustomer(CustomerModel data)
        {
            NpgsqlConnection npgsqlConnection1 = conn.ConnectBD(_configuration);
            try
            {
                using (SqlConnection sqlcn = new SqlConnection(npgsqlConnection1.ConnectionString))
                {
                    await sqlcn.OpenAsync();
                    string query = @$"UPDATE [dbo].[Customer] SET Name = '{data.Name}' WHERE CustomerId = {data.CustomerId}";

                    SqlCommand cmdT_Pt = new SqlCommand(query, sqlcn);
                    SqlDataReader sqldr = await cmdT_Pt.ExecuteReaderAsync();
                    List<CustomerModel> listaCus = new List<CustomerModel>();
                    if (sqldr.HasRows)
                    {
                        r.Data = listaCus;
                        r.Flag = true;
                        r.Message = "El usuario fue actualizado";
                        r.Status = 200;
                        return r;
                    }
                    await sqldr.CloseAsync();

                    r.Data = listaCus;
                    r.Flag = true;
                    r.Message = "No se puedo actualizar el usuario";
                    r.Status = 200;
                    return r;
                }
            }
            catch (Exception ex)
            {
                r.Flag = false;
                r.Status = 500;
                r.Message = "Error al Actualizar los Customer  - " + ex.Message;
                return r;
            }

        }
        #endregion

        #region Eliminar customer
        public async Task<ReplyLogin> DeleteCustomer(int id)
        {
            NpgsqlConnection npgsqlConnection1 = conn.ConnectBD(_configuration);
            try
            {
                using (SqlConnection sqlcn = new SqlConnection(npgsqlConnection1.ConnectionString))
                {
                    await sqlcn.OpenAsync();
                    string query = @$"DELETE [dbo].[Post] WHERE CustomerId = {id}";

                    SqlCommand cmdT_Pt = new SqlCommand(query, sqlcn);
                    SqlDataReader sqldr = await cmdT_Pt.ExecuteReaderAsync();

                    query = @$"DELETE [dbo].[Customer] WHERE CustomerId = {id}";

                    cmdT_Pt = new SqlCommand(query, sqlcn);
                    sqldr = await cmdT_Pt.ExecuteReaderAsync();
                    await sqldr.CloseAsync();

                    r.Data = null;
                    r.Flag = true;
                    r.Message = "Custer eliminado";
                    r.Status = 200;
                    return r;
                }
            }
            catch (Exception ex)
            {
                r.Flag = false;
                r.Status = 500;
                r.Message = "Error al Eliminar los Customer  - " + ex.Message;
                return r;
            }

        }
        #endregion
    }
}
