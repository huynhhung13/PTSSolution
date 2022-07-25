using PTS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace PTS.Data.Interfaces
{
    public interface IAdminDAO
    {
        //CRUD customer

        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task CreateCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int id);

        //CRUD person

        Task<IEnumerable<Person>> GetPeople();
        Task<IEnumerable<Person>> GetAdministrator();
        Task<Person> GetPerson(int id);
        Task CreatePerson(Person Person);
        Task UpdatePerson(Person Person);
        Task DeletePerson(int id);

        //CRUD projects

        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(Guid id);
        Task CreateProject(Project project);
        Task UpdateProject(Project project);
        Task DeleteProject(Guid id);

        //CRUD tasks

        Task<IEnumerable<Data.Models.Task>> GetTasks();
        Task<Data.Models.Task> GetTask(Guid id);
        Task CreateTask(Data.Models.Task task);
        Task UpdateTask(Data.Models.Task task);
        Task DeleteTask(Guid id);

        //CRUD team
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeam(int id);
        Task DeleteTeam(int id);
        Task CreateTeam(Team team);
        Task UpdateTeam(Team team);

        //Status
        Task<IEnumerable<Status>> GetStatuses();
    }
}
