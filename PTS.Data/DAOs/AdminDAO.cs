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
    public class AdminDAO : IAdminDAO
    {
        private readonly PTSContext db = new PTSContext();

        //CRUD Customers

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                var customers = await db.Customers.ToListAsync();
                return customers.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Customer> GetCustomer(int id)
        {
            var customer = db.Customers
                .Include(p => p.Projects).ThenInclude(pro => pro.Administrator)
                .Where(p => p.CustomerId == id)
                .FirstOrDefault();
            if (customer == null)
            {
                return null;
            }
            return customer;
        }
        public async Task DeleteCustomer(int id)
        {
            try
            {
                Customer customer = await db.Customers.FindAsync(id);
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            try
            {

                db.SaveChanges();
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateCustomer(Customer customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        //CRUD Person

        public async Task<IEnumerable<Person>> GetPeople()
        {
            try
            {
                var people = await db.People.Include(p => p.Teams)
                    .Include(p => p.TeamsNavigation)
                    .ToListAsync();
                return people.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Person>> GetAdministrator()
        {
            try
            {
                var people = await db.People.Where(x => x.IsAdministrator == true).ToListAsync();
                return people.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Person> GetPerson(int id)
        {
            var person = db.People
                .Include(p => p.TeamsNavigation)
                .Include(p => p.Teams)
                .Where(p => p.UserId == id)
                .FirstOrDefault();
            if (person == null)
            {
                return null;
            }
            return person;
        }
        public async Task DeletePerson(int id)
        {
            try
            {
                Person person = await db.People.FindAsync(id);
                db.People.Remove(person);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task CreatePerson(Person person)
        {
            db.People.Add(person);
            try
            {

                db.SaveChanges();
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdatePerson(Person person)
        {
            try
            {
                db.Entry(person).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        //CRUD Projects

        public async Task<IEnumerable<Project>> GetProjects()
        {
            try
            {
                var projects = await db.Projects.ToListAsync();
                return projects.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Project> GetProject(Guid id)
        {
            var project = db.Projects
                .Include(p => p.Customer)
                .Include(p => p.Administrator)
                .Include(p => p.Tasks).ThenInclude(task => task.Status)
                .Where(p => p.ProjectId == id)
                .FirstOrDefault();
            if (project == null)
            {
                return null;
            }
            return project;
        }
        public async Task DeleteProject(Guid id)
        {
            try
            {
                Project project = await db.Projects.FindAsync(id);
                db.Projects.Remove(project);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateProject(Project project)
        {
            project.ProjectId = Guid.NewGuid();
            project.ActualStartDate = null;
            project.ActualEndDate = null;
            project.Completed = false;
            db.Projects.Add(project);
            try
            {
                
                db.SaveChanges();
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateProject(Project project)
        {
            try
            {
                db.Entry(project).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        //CRUD Projects

        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            try
            {
                var tasks = await db.Tasks.ToListAsync();
                return tasks.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Models.Task> GetTask(Guid id)
        {
            var task = db.Tasks
                .Include(p => p.Project)
                .Include(p => p.Status)
                .Include(p => p.Team).ThenInclude(team => team.TeamLeader)
                .Include(p =>p.Team).ThenInclude(team => team.Users)
                .Include(p => p.Predecessors)
                .Include(p => p.Subtasks)
                .Where(p => p.TaskId == id)
                .FirstOrDefault();
            if (task == null)
            {
                return null;
            }
            return task;
        }
        public async Task DeleteTask(Guid id)
        {
            try
            {
                Models.Task task = await db.Tasks.FindAsync(id);
                db.Tasks.Remove(task);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateTask(Models.Task task)
        {
            task.TaskId = Guid.NewGuid();
            task.ActualDateStarted = null;
            task.ActualDateCompleted = null;
            task.StatusId = 1;
            task.PercentCompleted = 0;
            db.Tasks.Add(task);
            try
            {
                db.SaveChanges();
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateTask(Models.Task task)
        {
            try
            {
                db.Entry(task).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        //CRUD Teanm

        public async Task<IEnumerable<Team>> GetTeams()
        {
            try
            {
                var teams = await db.Teams.Include(t => t.TeamLeader).ToListAsync();
                return teams.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Team> GetTeam(int id)
        {
            var team = db.Teams
                .Include(p => p.TeamLeader)
                .Include(p => p.Users)
                .Include(p => p.Tasks).ThenInclude(task => task.Status)
                .Where(p => p.TeamId == id)
                .FirstOrDefault();
            if (team == null)
            {
                return null;
            }
            return team;
        }
        public async Task DeleteTeam(int id)
        {
            try
            {
                Team team = await db.Teams.FindAsync(id);
                db.Teams.Remove(team);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateTeam(Team team)
        {
            db.Teams.Add(team);
            try
            {

                db.SaveChanges();
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateTeam(Team team)
        {
            try
            {
                db.Entry(team).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        //Status
        public async Task<IEnumerable<Status>> GetStatuses()
        {
            try
            {
                var statuses = await db.Statuses.ToListAsync();
                return statuses.AsQueryable().Reverse();
            }
            catch
            {
                throw;
            }
        }
    }
}
