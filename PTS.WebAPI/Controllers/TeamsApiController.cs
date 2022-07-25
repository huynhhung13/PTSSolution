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
    public class TeamsApiController : ControllerBase
    {
        private readonly IAdminDAO _iAdminDAO = new AdminDAO();
        [HttpGet]
        [Route("GetList")]
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _iAdminDAO.GetTeams();
        }
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Team> GetTeam(int id)
        {
            var result = await _iAdminDAO.GetTeam(id);
            return result;
        }
        [HttpPost, Route("Create")]
        public async Task CreateAsync([FromBody] Team team)
        {
            await _iAdminDAO.CreateTeam(team);
        }
        [HttpPut, Route("Update")]
        public async Task UpdateAsync([FromBody] Team team)
        {
            await _iAdminDAO.UpdateTeam(team);
        }
        [HttpDelete, Route("Delete/{id}")]
        public async Task DeleteConfirmedAsync(int id)
        {
            await _iAdminDAO.DeleteTeam(id);
        }
    }
}
