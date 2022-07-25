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
    public class TeamsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7285/api/");
        HttpClient client;
        public TeamsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = null;
            HttpResponseMessage result = client.GetAsync(client.BaseAddress + "TeamsApi/getlist").Result;
            if (result.IsSuccessStatusCode)
            {
                string data = result.Content.ReadAsStringAsync().Result;
                teams = JsonConvert.DeserializeObject<IList<Team>>(data);
            }
            else
            {
                teams = Enumerable.Empty<Team>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View(teams);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Team team = null;
            var result = await client.GetAsync(client.BaseAddress + "TeamsApi/details/" + id);
            if (result.IsSuccessStatusCode)
            {
                team = await result.Content.ReadAsAsync<Team>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            List<SelectListItem> isExternal = new()
            {
                new SelectListItem { Value = "True", Text = "Nhóm Outsource" },
                new SelectListItem { Value = "False", Text = "Nhóm Product" },
            };
            ViewBag.isExternal = isExternal;
            //ViewBag teammember
            IEnumerable<Person> people = null;
            HttpResponseMessage result = client.GetAsync(client.BaseAddress + "PeopleApi/getlist").Result;
            if (result.IsSuccessStatusCode)
            {
                string data = result.Content.ReadAsStringAsync().Result;
                people = JsonConvert.DeserializeObject<IList<Person>>(data);
                List<SelectListItem> peopleList = new List<SelectListItem>();
                foreach (var item in people)
                {
                    peopleList.Add(new SelectListItem { Value = item.UserId.ToString(), Text = item.Name });
                };
                ViewBag.TeamLeaderId = peopleList;
            }
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Name,Location,TeamLeaderId,IsExternal")] Team team)
        {
            string data = JsonConvert.SerializeObject(team);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "TeamsApi/create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Thông tin không hợp lệ!");
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<SelectListItem> isExternal = new()
            {
                new SelectListItem { Value = "True", Text = "Nhóm Outsource" },
                new SelectListItem { Value = "False", Text = "Nhóm Product" },
            };
            ViewBag.isExternal = isExternal;
            //ViewBag teammember
            IEnumerable<Person> people = null;
            HttpResponseMessage presult = client.GetAsync(client.BaseAddress + "PeopleApi/getlist").Result;
            if (presult.IsSuccessStatusCode)
            {
                string pdata = presult.Content.ReadAsStringAsync().Result;
                people = JsonConvert.DeserializeObject<IList<Person>>(pdata);
                List<SelectListItem> peopleList = new List<SelectListItem>();
                foreach (var pitem in people)
                {
                    peopleList.Add(new SelectListItem { Value = pitem.UserId.ToString(), Text = pitem.Name });
                };
                ViewBag.TeamLeaderId = peopleList;
            }
            if (id == null)
            {
                return NotFound();
            }
            Team team = null;
            var result = await client.GetAsync($"TeamsApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                team = await result.Content.ReadAsAsync<Team>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name,Location,TeamLeaderId,IsExternal")] Team team)
        {
            var response = await client.PutAsJsonAsync(client.BaseAddress + "TeamsApi/update", team);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Thông tin không hợp lệ xin hãy thử lại");
                List<SelectListItem> isExternal = new()
                {
                new SelectListItem { Value = "True", Text = "Nhóm Outsource" },
                new SelectListItem { Value = "False", Text = "Nhóm Product" },
                };
                ViewBag.isExternal = isExternal;
                //ViewBag teammember
                IEnumerable<Person> people = null;
                HttpResponseMessage presult = client.GetAsync(client.BaseAddress + "PeopleApi/getlist").Result;
                if (presult.IsSuccessStatusCode)
                {
                    string pdata = presult.Content.ReadAsStringAsync().Result;
                    people = JsonConvert.DeserializeObject<IList<Person>>(pdata);
                    List<SelectListItem> peopleList = new List<SelectListItem>();
                    foreach (var pitem in people)
                    {
                        peopleList.Add(new SelectListItem { Value = pitem.UserId.ToString(), Text = pitem.Name });
                    };
                    ViewBag.TeamLeaderId = peopleList;
                }
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Team team = null;
            var result = await client.GetAsync($"TeamsApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                team = await result.Content.ReadAsAsync<Team>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await client.DeleteAsync($"TeamsApi/delete/{id}");
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
