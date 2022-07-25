using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.Data.DAOs;
using PTS.Data.Interfaces;
using PTS.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly IAdminDAO _iAdminDAO = new AdminDAO();
        [HttpGet]
        [Route("GetList")]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _iAdminDAO.GetCustomers();
        }
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Customer> GetCustomer(int id)
        {
            var result = await _iAdminDAO.GetCustomer(id);
            return result;
        }
        [HttpPost, Route("Create")]
        public async Task CreateAsync([FromBody] Customer customer)
        {
            await _iAdminDAO.CreateCustomer(customer);
        }
        [HttpPut, Route("Update")]
        public async Task UpdateAsync([FromBody] Customer customer)
        {
            await _iAdminDAO.UpdateCustomer(customer);
        }
        [HttpDelete, Route("Delete/{id}")]
        public async Task DeleteConfirmedAsync(int id)
        {
            await _iAdminDAO.DeleteCustomer(id);
        }
    }
}
