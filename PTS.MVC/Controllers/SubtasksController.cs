using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTS.Data.Models;

namespace PTS.MVC.Controllers
{
    public class SubtasksController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7285/api/");
        HttpClient client;
        public SubtasksController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Subtasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Subtask subtask = null;
            var result = await client.GetAsync(client.BaseAddress + "TeamLeaderApi/SubtaskDetails/" + id);
            if (result.IsSuccessStatusCode)
            {
                subtask = await result.Content.ReadAsAsync<Subtask>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            if (subtask == null)
            {
                return NotFound();
            }
            return View(subtask);
        }
    }
}
