using PTS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Data.Interfaces
{
    public interface ICustomerDAO
    {
        Task<IEnumerable<Project>> GetProjects(int id);
        Task<Project> GetProject(Guid projectId);
    }
}
