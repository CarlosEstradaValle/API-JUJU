using API.Interfaces.Customer;
using API.Interfaces.Logs;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _Customer;
        public CustomerController(ICustomer customer)
        {
            _Customer = customer;
        }

        #region Método para consultar todos los logs
        // GET: api/GetCustomer
        /// <summary>
        /// Obtener todos los Customer
        /// </summary>
        /// <remarks>
        /// Método para consultar todos los Customer
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        [HttpGet]
        [Route("api/GetCustomer")]
        public async Task<ReplyLogin> GetCustomer()
        {
            var response = await _Customer.GetCustomer();

            return response;
        }
        #endregion

        #region Método para consultar todos los logs
        // POST: api/CreateCustomer
        /// <summary>
        /// Crear customer
        /// </summary>
        /// <remarks>
        /// Método para Crear customer
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        [HttpPost]
        [Route("api/CreateCustomer")]
        public async Task<ReplyLogin> CreateCustomer(CustomerModel data)
        {
            var response = await _Customer.CreateCustomer(data);

            return response;
        }
        #endregion

        #region Método para consultar todos los logs
        // POST: api/UpdateCustomer
        /// <summary>
        /// Actualizar customer
        /// </summary>
        /// <remarks>
        /// Método para Actualizar customer
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        [HttpPost]
        [Route("api/UpdateCustomer")]
        public async Task<ReplyLogin> UpdateCustomer(CustomerModel data)
        {
            var response = await _Customer.UpdateCustomer(data);

            return response;
        }
        #endregion

        #region Método para consultar todos los logs
        // DELETE: api/DeleteCustomer
        /// <summary>
        /// Eliminar customer
        /// </summary>
        /// <remarks>
        /// Método para eliminar customer
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        [HttpDelete]
        [Route("api/DeleteCustomer")]
        public async Task<ReplyLogin> DeleteCustomer(int id)
        {
            var response = await _Customer.DeleteCustomer(id);

            return response;
        }
        #endregion
    }
}
