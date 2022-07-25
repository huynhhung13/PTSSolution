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
    public class ProjectsApiController : ControllerBase
    {
        private readonly IAdminDAO _iAdminDAO = new AdminDAO();
        [HttpGet]
        [Route("GetList")]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _iAdminDAO.GetProjects();
        }
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Project> GetProject(Guid id)
        {
            var result = await _iAdminDAO.GetProject(id);
            return result;
        }
        [HttpPost, Route("Create")]
        public async Task CreateAsync([FromBody]Project project)
        {
            await _iAdminDAO.CreateProject(project);
        }
        [HttpPut, Route("Update")]
        public async Task UpdateAsync([FromBody] Project project)
        {
            await _iAdminDAO.UpdateProject(project);
        }
        [HttpDelete, Route("Delete/{id}")]
        public async Task DeleteConfirmedAsync(Guid id)
        {
            await _iAdminDAO.DeleteProject(id);
        }
    }
}
