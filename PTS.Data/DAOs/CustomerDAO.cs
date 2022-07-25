using Microsoft.EntityFrameworkCore;
using PTS.Data.Interfaces;
using PTS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Data.DAOs
{
    public class CustomerDAO : ICustomerDAO
    {
        private readonly PTSContext db = new PTSContext();
        public async Task<IEnumerable<Project>> GetProjects(int id)
        {
            try
            {
                var projects = await db.Projects.Where(p => p.CustomerId == id)
                    .ToListAsync();
                return projects.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Project> GetProject(Guid projectId)
        {
            var project = db.Projects
                .Include(p => p.Customer)
                .Include(p => p.Administrator)
                .Include(p => p.Tasks).ThenInclude(task => task.Status)
                .Where(p => p.ProjectId == projectId)
                .FirstOrDefault();
            if (project == null)
            {
                return null;
            }
            return project;
        }
    }
}
