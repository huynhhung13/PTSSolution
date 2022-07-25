using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PTS.Data.Models;

namespace PTS.MVC.Controllers
{
    public class TasksController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7285/api/");
        HttpClient client;
        public TasksController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            IEnumerable<Data.Models.Task> tasks = null;
            HttpResponseMessage result = client.GetAsync(client.BaseAddress + "TasksApi/getlist").Result;
            if (result.IsSuccessStatusCode)
            {
                string data = result.Content.ReadAsStringAsync().Result;
                tasks = JsonConvert.DeserializeObject<IList<Data.Models.Task>>(data);
            }
            else
            {
                tasks = Enumerable.Empty<Data.Models.Task>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View(tasks);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            Data.Models.Task task = null;
            var result = await client.GetAsync(client.BaseAddress + "TasksApi/details/" + id);
            if (result.IsSuccessStatusCode)
            {
                task = await result.Content.ReadAsAsync<Data.Models.Task>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            //ViewBag project
            IEnumerable<Project> projects = null;
            HttpResponseMessage presult = client.GetAsync(client.BaseAddress + "ProjectsApi/getlist").Result;
            if (presult.IsSuccessStatusCode)
            {
                string pdata = presult.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<IList<Project>>(pdata);
                List<SelectListItem> projectList = new List<SelectListItem>();
                foreach (var pitem in projects)
                {
                    projectList.Add(new SelectListItem { Value = pitem.ProjectId.ToString(), Text = pitem.Name });
                };
                ViewBag.projectList = projectList;
            }
            //ViewBag team
            IEnumerable<Team> teams = null;
            HttpResponseMessage tresult = client.GetAsync(client.BaseAddress + "TeamsApi/getlist").Result;
            if (tresult.IsSuccessStatusCode)
            {
                string tdata = tresult.Content.ReadAsStringAsync().Result;
                teams = JsonConvert.DeserializeObject<IList<Team>>(tdata);
                List<SelectListItem> teamList = new List<SelectListItem>();
                foreach (var tItem in teams)
                {
                    teamList.Add(new SelectListItem { Value = tItem.TeamId.ToString(), Text = tItem.Name });
                };
                ViewBag.teamList = teamList;
            }
            
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,Name,ExpectedDateStarted,ExpectedDateCompleted,ProjectId,TeamId,StatusId")] Data.Models.Task task)
        {
            string data = JsonConvert.SerializeObject(task);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "TasksApi/create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details","Projects", new {id = task.ProjectId});
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Sai mã người quản trị hoặc mã khách hàng! Xin vui lòng nhập lại!");
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //ViewBag project
            IEnumerable<Project> projects = null;
            HttpResponseMessage presult = client.GetAsync(client.BaseAddress + "ProjectsApi/getlist").Result;
            if (presult.IsSuccessStatusCode)
            {
                string pdata = presult.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<IList<Project>>(pdata);
                List<SelectListItem> projectList = new List<SelectListItem>();
                foreach (var pitem in projects)
                {
                    projectList.Add(new SelectListItem { Value = pitem.ProjectId.ToString(), Text = pitem.Name });
                };
                ViewBag.projectList = projectList;
            }
            //ViewBag team
            IEnumerable<Team> teams = null;
            HttpResponseMessage tresult = client.GetAsync(client.BaseAddress + "TeamsApi/getlist").Result;
            if (tresult.IsSuccessStatusCode)
            {
                string tdata = tresult.Content.ReadAsStringAsync().Result;
                teams = JsonConvert.DeserializeObject<IList<Team>>(tdata);
                List<SelectListItem> teamList = new List<SelectListItem>();
                foreach (var tItem in teams)
                {
                    teamList.Add(new SelectListItem { Value = tItem.TeamId.ToString(), Text = tItem.Name });
                };
                ViewBag.teamList = teamList;
            }
            //ViewBag status
            IEnumerable<Status> statuses = null;
            HttpResponseMessage sresult = client.GetAsync(client.BaseAddress + "TasksApi/getstatuslist").Result;
            if (presult.IsSuccessStatusCode)
            {
                string sdata = sresult.Content.ReadAsStringAsync().Result;
                statuses = JsonConvert.DeserializeObject<IList<Status>>(sdata);
                List<SelectListItem> statusList = new List<SelectListItem>();
                foreach (var sItem in statuses)
                {
                    statusList.Add(new SelectListItem { Value = sItem.StatusId.ToString(), Text = sItem.Name });
                };
                ViewBag.status = statusList;
            }
            if (id == null)
            {
                return NotFound();
            }
            Data.Models.Task task = null;
            var result = await client.GetAsync($"TasksApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                task = await result.Content.ReadAsAsync<Data.Models.Task>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TaskId,Name,ExpectedDateStarted,ExpectedDateCompleted,ActualDateStarted,ActualDateCompleted,ProjectId,TeamId,StatusId,PercentCompleted")] Data.Models.Task task)
        {
            var response = await client.PutAsJsonAsync(client.BaseAddress + "TasksApi/update", task);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details","Projects", new {id = task.ProjectId});
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Thông tin không hợp lệ xin hãy thử lại");
                //ViewBag project
                IEnumerable<Project> projects = null;
                HttpResponseMessage presult = client.GetAsync(client.BaseAddress + "ProjectsApi/getlist").Result;
                if (presult.IsSuccessStatusCode)
                {
                    string pdata = presult.Content.ReadAsStringAsync().Result;
                    projects = JsonConvert.DeserializeObject<IList<Project>>(pdata);
                    List<SelectListItem> projectList = new List<SelectListItem>();
                    foreach (var pitem in projects)
                    {
                        projectList.Add(new SelectListItem { Value = pitem.ProjectId.ToString(), Text = pitem.Name });
                    };
                    ViewBag.projectList = projectList;
                }
                //ViewBag team
                IEnumerable<Team> teams = null;
                HttpResponseMessage tresult = client.GetAsync(client.BaseAddress + "TeamsApi/getlist").Result;
                if (tresult.IsSuccessStatusCode)
                {
                    string tdata = tresult.Content.ReadAsStringAsync().Result;
                    teams = JsonConvert.DeserializeObject<IList<Team>>(tdata);
                    List<SelectListItem> teamList = new List<SelectListItem>();
                    foreach (var tItem in teams)
                    {
                        teamList.Add(new SelectListItem { Value = tItem.TeamId.ToString(), Text = tItem.Name });
                    };
                    ViewBag.teamList = teamList;
                }
                //ViewBag status
                IEnumerable<Status> statuses = null;
                HttpResponseMessage sresult = client.GetAsync(client.BaseAddress + "TasksApi/getstatuslist").Result;
                if (presult.IsSuccessStatusCode)
                {
                    string sdata = sresult.Content.ReadAsStringAsync().Result;
                    statuses = JsonConvert.DeserializeObject<IList<Status>>(sdata);
                    List<SelectListItem> statusList = new List<SelectListItem>();
                    foreach (var sItem in statuses)
                    {
                        statusList.Add(new SelectListItem { Value = sItem.StatusId.ToString(), Text = sItem.Name });
                    };
                    ViewBag.status = statusList;
                }
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Data.Models.Task task = null;
            var result = await client.GetAsync($"TasksApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                task = await result.Content.ReadAsAsync<Data.Models.Task>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tresult = await client.GetAsync($"TasksApi/details/{id}");
            if (tresult.IsSuccessStatusCode)
            {
                Data.Models.Task task = await tresult.Content.ReadAsAsync<Data.Models.Task>();
                var response = await client.DeleteAsync($"TasksApi/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "Projects", new { id = task.ProjectId});
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
