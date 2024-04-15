using API.Core.Context;
using API.Interfaces.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers.Logs
{
    public class LogsController : Controller
    {

        private readonly ILogs _Logs;
        public LogsController(ILogs  logs)
        {
            _Logs = logs;
        }

        #region Método para consultar todos los logs
        // GET: api/GetLogs
        /// <summary>
        /// Obtener todos los logs
        /// </summary>
        /// <remarks>
        /// Método para consultar todos los logs
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        [HttpGet]
        [Route("api/GetLogs")]
        public async Task<ReplyLogin> GetLogs()
        {
            var response = await _Logs.GetLogs();

            return response;
        }
        #endregion

    }
}
