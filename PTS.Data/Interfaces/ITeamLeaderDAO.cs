using PTS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace PTS.Data.Interfaces
{
    public interface ITeamLeaderDAO
    {
        Task<IEnumerable<Data.Models.Task>> GetTasksOfTeam(int id);
        Task<IEnumerable<Subtask>> GetSubtasks(Guid id);
        Task<Subtask> GetSubtask(int id);
        Task DeleteSubtask(int id);
        Task CreateSubtask(Subtask subtask);
        Task UpdateSubtask(Subtask subtask);

    }
}
