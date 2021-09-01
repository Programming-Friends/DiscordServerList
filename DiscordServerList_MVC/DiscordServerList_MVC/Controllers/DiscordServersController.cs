using DiscordServerListLib.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordServerList_MVC.Controllers
{
    public class DiscordServersController : Controller
    {
        private readonly IDiscordServerRepository _discordServerRepository;

        public DiscordServersController(IDiscordServerRepository discordServerRepository)
        {
            _discordServerRepository = discordServerRepository;
        }

        // GET: DiscordServersController
        public async Task<IActionResult> Index()
        {
            var servers = await _discordServerRepository.GetDiscordServers();
            return View(servers);
        }

        // GET: DiscordServersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DiscordServersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscordServersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscordServersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DiscordServersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscordServersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DiscordServersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
