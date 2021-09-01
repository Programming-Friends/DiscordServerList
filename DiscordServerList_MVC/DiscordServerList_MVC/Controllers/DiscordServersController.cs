/*
MIT License
Copyright(c) 2021 Kyle Givler
https://github.com/JoyfulReaper
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using DiscordServerListLib.Data;
using DiscordServerListLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<DiscordUser> _userManager;

        public DiscordServersController(IDiscordServerRepository discordServerRepository,
            UserManager<DiscordUser> userManager)
        {
            _discordServerRepository = discordServerRepository;
            _userManager = userManager;
        }

        // GET: DiscordServersController
        public async Task<IActionResult> Index()
        {
            var servers = await _discordServerRepository.GetDiscordServers();
            return View(servers);
        }

        // GET: DiscordServersController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: DiscordServersController/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiscordServersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,InviteLink")] DiscordServer discordServer)
        {
            if (ModelState.IsValid)
            {
                discordServer.Created = DateTime.Now;
                discordServer.CreatorId = _userManager.GetUserId(User);

                await _discordServerRepository.InsertDiscordServer(discordServer);

                return RedirectToAction(nameof(Index));
            }

            return View(discordServer);
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
