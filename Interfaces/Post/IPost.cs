using API.Models;

namespace API.Interfaces.Post
{
    public interface IPost
    {
        Task<ReplyLogin> GetPost();
        Task<ReplyLogin> CreatePots(PostModel data);
    }
}
