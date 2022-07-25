using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.Data.DAOs;
using PTS.Data.Interfaces;
using PTS.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace PTS.WebAPI.Controllers.TeamLeaderApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamLeaderApiController : ControllerBase
    {
        private readonly ITeamLeaderDAO _iteamLeaderDAO = new TeamLeaderDAO();
        [HttpGet]
        [Route("GetTaskOfTeam")]
        public async Task<IEnumerable<Data.Models.Task>> GetTasksOfTeam(int id)
        {
            return await _iteamLeaderDAO.GetTasksOfTeam(id);
        }
        [HttpGet]
        [Route("GetSubtasks/{id}")]
        public async Task<IEnumerable<Subtask>> GetSubtasks(Guid id)
        {
            var result = await _iteamLeaderDAO.GetSubtasks(id);
            return result;
        }
        [HttpGet]
        [Route("SubtaskDetails/{id}")]
        public async Task<Subtask> GetSubtask(int id)
        {
            var result = await _iteamLeaderDAO.GetSubtask(id);
            return result;
        }
        [HttpPost, Route("CreateSubtask")]
        public async Task CreateAsync([FromBody] Subtask subtask)
        {
            await _iteamLeaderDAO.CreateSubtask(subtask);
        }
        [HttpPut, Route("UpdateSubtask")]
        public async Task UpdateAsync([FromBody] Subtask subtask)
        {
            await _iteamLeaderDAO.UpdateSubtask(subtask);
        }
        [HttpDelete, Route("Delete/{id}")]
        public async Task DeleteConfirmedAsync(int id)
        {
            await _iteamLeaderDAO.DeleteSubtask(id);
        }
    }
}
