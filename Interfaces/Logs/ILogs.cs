using API.Models;

namespace API.Interfaces.Logs
{
    public interface ILogs
    {
        Task<ReplyLogin> GetLogs();
    }
}
