using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.Data.DAOs;
using PTS.Data.Interfaces;
using PTS.Data.Models;

namespace PTS.WebAPI.Controllers.CustomerApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingApiController : ControllerBase
    {
        private readonly ICustomerDAO _icustomerDAO = new CustomerDAO();
        [HttpGet]
        [Route("GetProjects/{id}")]
        public async Task<IEnumerable<Project>> GetProjects(int id)
        {
            return await _icustomerDAO.GetProjects(id);
        }
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Project> GetProject(Guid id)
        {
            var result = await _icustomerDAO.GetProject(id);
            return result;
        }
    }
}
