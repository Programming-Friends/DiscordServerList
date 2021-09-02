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
    public class CatagoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<DiscordUser> _userManager;

        public CatagoriesController(ICategoryRepository categoryRepository,
            UserManager<DiscordUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        // GET: CatagoriesController
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetCategories();

            return View(categories);
        }

        // GET: CatagoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CatagoriesController/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatagoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Created = DateTime.Now;
                category.CreatorId = _userManager.GetUserId(User);

                await _categoryRepository.InsertCategory(category);

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: CatagoriesController/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CatagoriesController/Edit/5
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

        // GET: CatagoriesController/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CatagoriesController/Delete/5
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
