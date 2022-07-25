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
    public class TasksApiController : ControllerBase
    {
        private readonly IAdminDAO _iAdminDAO = new AdminDAO();
        [HttpGet]
        [Route("GetList")]
        public async Task<IEnumerable<Data.Models.Task>> GetTasks()
        {
            return await _iAdminDAO.GetTasks();
        }
        [HttpGet]
        [Route("GetStatusList")]
        public async Task<IEnumerable<Status>> GetStatusList()
        {
            return await _iAdminDAO.GetStatuses();
        }
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Data.Models.Task> GetTask(Guid id)
        {
            var result = await _iAdminDAO.GetTask(id);
            return result;
        }
        [HttpPost, Route("Create")]
        public async Task CreateAsync([FromBody] Data.Models.Task task)
        {
            await _iAdminDAO.CreateTask(task);
        }
        [HttpPut, Route("Update")]
        public async Task UpdateAsync([FromBody] Data.Models.Task task)
        {
            await _iAdminDAO.UpdateTask(task);
        }
        [HttpDelete, Route("Delete/{id}")]
        public async Task DeleteConfirmedAsync(Guid id)
        {
            await _iAdminDAO.DeleteTask(id);
        }
    }
}
