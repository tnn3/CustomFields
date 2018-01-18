using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class CustomFieldController : Controller
    {
        private readonly ICustomFieldRepository _customFieldRepository;

        public CustomFieldController(ICustomFieldRepository customFieldRepository)
        {
            _customFieldRepository = customFieldRepository;
        }

        // GET: CustomField
        public async Task<ActionResult> Index()
        {
            return View(await _customFieldRepository.AllAsync());
        }

        // GET: CustomField/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomField/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomField/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomField/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomField/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomField/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}