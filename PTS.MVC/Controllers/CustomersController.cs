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
    public class CustomersController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7285/api/");
        HttpClient client;
        public CustomersController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            IEnumerable<Customer> customers = null;
            HttpResponseMessage result = client.GetAsync(client.BaseAddress + "CustomersApi/getlist").Result;
            if (result.IsSuccessStatusCode)
            {
                string data = result.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<IList<Customer>>(data);
            }
            else
            {
                customers = Enumerable.Empty<Customer>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Customer customer = null;
            var result = await client.GetAsync(client.BaseAddress + "CustomersApi/details/" + id);
            if (result.IsSuccessStatusCode)
            {
                customer = await result.Content.ReadAsAsync<Customer>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Name,Username,Password,Email,TelephoneNo,Company,Country,Address")] Customer customer)
        {
            string data = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "CustomersApi/create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Thông tin không hợp lệ!");
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = null;
            var result = await client.GetAsync($"CustomersApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                customer = await result.Content.ReadAsAsync<Customer>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Name,Username,Password,Email,TelephoneNo,Company,Country,Address")] Customer customer)
        {
            var response = await client.PutAsJsonAsync(client.BaseAddress + "CustomersApi/update", customer);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Thông tin không hợp lệ xin hãy thử lại");
            }
            return View(customer);
        }

        //// GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = null;
            var result = await client.GetAsync($"CustomersApi/details/{id}");
            if (result.IsSuccessStatusCode)
            {
                customer = await result.Content.ReadAsAsync<Customer>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer); 
        }

        //// POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await client.DeleteAsync($"CustomersApi/delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError(string.Empty, "Người dùng đã đăng ký dự án không thể xóa");
            return RedirectToAction(nameof(Index));
        }
    }
}
