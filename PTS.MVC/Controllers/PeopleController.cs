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
    public class PeopleController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7285/api/");
        HttpClient client;
        public PeopleController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            IEnumerable<Person> people = null;
            HttpResponseMessage result = client.GetAsync(client.BaseAddress + "PeopleApi/getlist").Result;
            if (result.IsSuccessStatusCode)
            {
                string data = result.Content.ReadAsStringAsync().Result;
                people = JsonConvert.DeserializeObject<IList<Person>>(data);
            }
            else
            {
                people = Enumerable.Empty<Person>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View(people);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            Person person = null;
            var result = await client.GetAsync(client.BaseAddress + "PeopleApi/details/" + id);
            if (result.IsSuccessStatusCode)
            {
                person = await result.Content.ReadAsAsync<Person>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            List<SelectListItem> isAdmin = new()
            {
                new SelectListItem { Value = "True", Text = "Project Manager" },
                new SelectListItem { Value = "False", Text = "Nhân viên" },
            };
            ViewBag.isAdmin = isAdmin;
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Username,Password,Email,TelephoneNo,IsAdministrator")] Person person)
        {
            
            string data = JsonConvert.SerializeObject(person);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "PeopleApi/create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Thông tin không hợp lệ!");
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<SelectListItem> isAdmin = new()
            {
                new SelectListItem { Value = "True", Text = "Project Manager" },
                new SelectListItem { Value = "False", Text = "Nhân viên" },
            };
            ViewBag.isAdmin = isAdmin;
            if (id == null)
            {
                return NotFound();
            }
            Person person = null;
            var result = await client.GetAsync($"PeopleApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                person = await result.Content.ReadAsAsync<Person>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,Username,Password,Email,TelephoneNo,IsAdministrator")] Person person)
        {
            var response = await client.PutAsJsonAsync(client.BaseAddress + "PeopleApi/update", person);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<SelectListItem> isAdmin = new()
            {
                new SelectListItem { Value = "True", Text = "Project Manager" },
                new SelectListItem { Value = "False", Text = "Nhân viên" },
            };
                ViewBag.isAdmin = isAdmin;
                ModelState.AddModelError(string.Empty, "Thông tin không hợp lệ xin hãy thử lại");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Person person = null;
            var result = await client.GetAsync($"PeopleApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                person = await result.Content.ReadAsAsync<Person>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await client.DeleteAsync($"PeopleApi/delete/{id}");
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
