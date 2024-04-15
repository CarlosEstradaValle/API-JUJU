using API.Models;

namespace API.Interfaces.Customer
{
    public interface ICustomer
    {
        Task<ReplyLogin> GetCustomer();
        Task<ReplyLogin> CreateCustomer(CustomerModel data);
        Task<ReplyLogin> UpdateCustomer(CustomerModel data);
        Task<ReplyLogin> DeleteCustomer(int id)

    }
}
