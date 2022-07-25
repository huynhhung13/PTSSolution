using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PTS.Data.Models;
using System.Net.Http;
using System.Text;

namespace PTS.MVC.Controllers
{
    public class ProjectsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7285/api/");
        HttpClient client;
        public ProjectsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            IEnumerable<Project> projects = null;
            HttpResponseMessage result = client.GetAsync(client.BaseAddress + "ProjectsApi/getlist").Result;
            if (result.IsSuccessStatusCode)
            {
                string data = result.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<IList<Project>>(data);
            }
            else
            {
                projects = Enumerable.Empty<Project>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View(projects);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            Project project = null;
            var result = await client.GetAsync(client.BaseAddress + "ProjectsApi/details/" + id);
            if (result.IsSuccessStatusCode)
            {
                project = await result.Content.ReadAsAsync<Project>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        //// GET: Projects/Create
        public IActionResult Create()
        {
            //ViewBag customer
            IEnumerable<Customer> customers = null;
            HttpResponseMessage cresult = client.GetAsync(client.BaseAddress + "CustomersApi/getlist").Result;
            if (cresult.IsSuccessStatusCode)
            {
                string cdata = cresult.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<IList<Customer>>(cdata);
                List<SelectListItem> customerList = new List<SelectListItem>();
                foreach (var citem in customers)
                {
                    customerList.Add(new SelectListItem { Value = citem.CustomerId.ToString(), Text = citem.Name });
                };
                ViewBag.customerList = customerList;
            }
            //ViewBag Admin
            IEnumerable<Person> admin = null;
            HttpResponseMessage aresult = client.GetAsync(client.BaseAddress + "PeopleApi/getadminlist").Result;
            if (aresult.IsSuccessStatusCode)
            {
                string adata = aresult.Content.ReadAsStringAsync().Result;
                admin = JsonConvert.DeserializeObject<IList<Person>>(adata);
                List<SelectListItem> adminList = new List<SelectListItem>();
                foreach (var aitem in admin)
                {
                    adminList.Add(new SelectListItem { Value = aitem.UserId.ToString(), Text = aitem.Name });
                };
                ViewBag.adminList = adminList;
            }

            return View();
        }

        //// POST: Projects/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ProjectId,Name,ExpectedStartDate,ExpectedEndDate,CustomerId,AdministratorId")] Project project)
        {
            
            string data = JsonConvert.SerializeObject(project);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "ProjectsApi/create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Sai mã người quản trị hoặc mã khách hàng! Xin vui lòng nhập lại!");
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Project project = null;
            var result = await client.GetAsync($"ProjectsApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                project = await result.Content.ReadAsAsync<Project>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (project == null)
            {
                return NotFound();
            }

            List<SelectListItem> completed = new()
            {
                new SelectListItem { Value = "True", Text = "Đã hoàn thành" },
                new SelectListItem { Value = "False", Text = "Chưa hoàn thành" },
            };
            ViewBag.completed = completed;
            //ViewBag customer
            IEnumerable<Customer> customers = null;
            HttpResponseMessage cresult = client.GetAsync(client.BaseAddress + "CustomersApi/getlist").Result;
            if (cresult.IsSuccessStatusCode)
            {
                string cdata = cresult.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<IList<Customer>>(cdata);
                List<SelectListItem> customerList = new List<SelectListItem>();
                foreach (var citem in customers)
                {
                    customerList.Add(new SelectListItem { Value = citem.CustomerId.ToString(), Text = citem.Name });
                };
                ViewBag.customerList = customerList;
            }
            //ViewBag Admin
            IEnumerable<Person> admin = null;
            HttpResponseMessage aresult = client.GetAsync(client.BaseAddress + "PeopleApi/getadminlist").Result;
            if (aresult.IsSuccessStatusCode)
            {
                string adata = aresult.Content.ReadAsStringAsync().Result;
                admin = JsonConvert.DeserializeObject<IList<Person>>(adata);
                List<SelectListItem> adminList = new List<SelectListItem>();
                foreach (var aitem in admin)
                {
                    adminList.Add(new SelectListItem { Value = aitem.UserId.ToString(), Text = aitem.Name });
                };
                ViewBag.adminList = adminList;
            }
            

            return View(project);
        }

        //// POST: Projects/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProjectId,Name,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,Completed,CustomerId,AdministratorId")] Project project)
        {
            var response = await client.PutAsJsonAsync(client.BaseAddress + "ProjectsApi/update", project);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details",new { id =  project.ProjectId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Thông tin không hợp lệ xin hãy thử lại");
                List<SelectListItem> completed = new()
                {
                new SelectListItem { Value = "True", Text = "Đã hoàn thành" },
                new SelectListItem { Value = "False", Text = "Chưa hoàn thành" },
                };
                ViewBag.completed = completed;
                //ViewBag customer
                IEnumerable<Customer> customers = null;
                HttpResponseMessage cresult = client.GetAsync(client.BaseAddress + "CustomersApi/getlist").Result;
                if (cresult.IsSuccessStatusCode)
                {
                    string cdata = cresult.Content.ReadAsStringAsync().Result;
                    customers = JsonConvert.DeserializeObject<IList<Customer>>(cdata);
                    List<SelectListItem> customerList = new List<SelectListItem>();
                    foreach (var citem in customers)
                    {
                        customerList.Add(new SelectListItem { Value = citem.CustomerId.ToString(), Text = citem.Name });
                    };
                    ViewBag.customerList = customerList;
                }
                //ViewBag Admin
                IEnumerable<Person> admin = null;
                HttpResponseMessage aresult = client.GetAsync(client.BaseAddress + "PeopleApi/getadminlist").Result;
                if (aresult.IsSuccessStatusCode)
                {
                    string adata = aresult.Content.ReadAsStringAsync().Result;
                    admin = JsonConvert.DeserializeObject<IList<Person>>(adata);
                    List<SelectListItem> adminList = new List<SelectListItem>();
                    foreach (var aitem in admin)
                    {
                        adminList.Add(new SelectListItem { Value = aitem.UserId.ToString(), Text = aitem.Name });
                    };
                    ViewBag.adminList = adminList;
                }
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Project project = null;
            var result = await client.GetAsync($"ProjectsApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                project = await result.Content.ReadAsAsync<Project>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await client.DeleteAsync($"ProjectsApi/delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            return RedirectToAction(nameof(Index));
        }


    }
}
