using API.Interfaces.Customer;
using API.Interfaces.Post;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Post
{
    public class PostController : Controller
    {
        private readonly IPost _Post;
        public PostController(IPost post)
        {
            _Post = post;
        }

        #region Método para consultar todos los logs
        // GET: api/GetPost
        /// <summary>
        /// Obtener todos los Customer
        /// </summary>
        /// <remarks>
        /// Método para consultar todos los Customer
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        [HttpGet]
        [Route("api/GetPost")]
        public async Task<ReplyLogin> GetPost()
        {
            var response = await _Post.GetPost();

            return response;
        }
        #endregion

        #region Método asincrono para crear los pots
        // POST: api/CreatePots
        /// <summary>
        /// Crear customer
        /// </summary>
        /// <remarks>
        /// Método para crear los pots
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        [HttpPost]
        [Route("api/CreatePots")]
        public async Task<ReplyLogin> CreatePots(PostModel data)
        {
            var response = await _Post.CreatePots(data);

            return response;
        }
        #endregion
    }
}
