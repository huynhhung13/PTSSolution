using Microsoft.EntityFrameworkCore;
using PTS.Data.Interfaces;
using PTS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace PTS.Data.DAOs
{
    public class TeamLeaderDAO : ITeamLeaderDAO
    {
        private readonly PTSContext db = new PTSContext();

        //CRUD Subtasks

        public async Task<IEnumerable<Data.Models.Task>> GetTasksOfTeam(int id)
        {
            try
            {
                var tasks = await db.Tasks.Include(s => s.Status)
                    .Include(s => s.Team)
                    .Include(s => s.Project)
                    .Include(s => s.Predecessors)
                    .Where(s => s.Team.TeamLeaderId == id)
                    .ToListAsync();
                return tasks.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Subtask>> GetSubtasks(Guid id)
        {
            try
            {
                var subtasks = await db.Subtasks.Include(s => s.Status)
                    .Include(s => s.TeamMember)
                    .Include(s => s.Task)
                    .Where(s => s.TaskId == id)
                    .ToListAsync();
                return subtasks.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Subtask> GetSubtask(int id)
        {
            var task = db.Subtasks
                .Include(p => p.TeamMember)
                .Include(p => p.Task)
                .Where(p => p.SubtaskId == id)
                .FirstOrDefault();
            if (task == null)
            {
                return null;
            }
            return task;
        }
        public async Task DeleteSubtask(int id)
        {
            try
            {
                Subtask subtask = await db.Subtasks.FindAsync(id);
                db.Subtasks.Remove(subtask);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateSubtask(Subtask subtask)
        {
            
            db.Subtasks.Add(subtask);
            try
            {
                db.SaveChanges();
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateSubtask(Subtask subtask)
        {
            try
            {
                db.Entry(subtask).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
