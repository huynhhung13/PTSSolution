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
    public class PeopleApiController : ControllerBase
    {
        private readonly IAdminDAO _iAdminDAO = new AdminDAO();
        [HttpGet]
        [Route("GetList")]
        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await _iAdminDAO.GetPeople();
        }
        [HttpGet]
        [Route("GetAdminList")]
        public async Task<IEnumerable<Person>> GetAdministrator()
        {
            return await _iAdminDAO.GetAdministrator();
        }
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Person> GetPerson(int id)
        {
            var result = await _iAdminDAO.GetPerson(id);
            return result;
        }
        [HttpPost, Route("Create")]
        public async Task CreateAsync([FromBody] Person person)
        {
            await _iAdminDAO.CreatePerson(person);
        }
        [HttpPut, Route("Update")]
        public async Task UpdateAsync([FromBody] Person person)
        {
            await _iAdminDAO.UpdatePerson(person);
        }
        [HttpDelete, Route("Delete/{id}")]
        public async Task DeleteConfirmedAsync(int id)
        {
            await _iAdminDAO.DeletePerson(id);
        }
    }
}
