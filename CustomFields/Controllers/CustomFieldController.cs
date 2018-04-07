using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Interfaces.Repositories;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CustomFieldController : Controller
    {
        private readonly ICustomFieldRepository _customFieldRepository;

        public CustomFieldController(ICustomFieldRepository customFieldRepository)
        {
            _customFieldRepository = customFieldRepository;
        }

        // GET: CustomFields
        public async Task<IActionResult> Index() {
            var fields = await _customFieldRepository.AllAsync();
            return View(fields.OrderBy(f => f.Sort));
        }

        // GET: CustomFields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customField = await _customFieldRepository.FindWithReferencesAsync(id.Value);
            if (customField == null)
            {
                return NotFound();
            }

            return View(customField);
        }

        // GET: CustomFields/Create
        public IActionResult Create()
        {
            return View(new CustomFieldViewModel());
        }

        // POST: CustomFields/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomFieldViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _customFieldRepository.Add(vm.CustomField);
            await _customFieldRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomFields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customField = await _customFieldRepository.FindWithReferencesAsync(id.Value);
            if (customField == null)
            {
                return NotFound();
            }

            var vm = new CustomFieldViewModel
            {
                CustomField = customField,
                HasExistingData = customField.Tasks.Any()
            };

            return View(vm);
        }

        // POST: CustomFields/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomFieldViewModel vm)
        {
            if (id != vm.CustomField.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(vm);
            try
            {
                _customFieldRepository.Update(vm.CustomField);
                await _customFieldRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_customFieldRepository.Exists(vm.CustomField.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomFields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customField = await _customFieldRepository.FindAsync(id.Value);
            if (customField == null)
            {
                return NotFound();
            }

            return View(customField);
        }

        // POST: CustomFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customField = await _customFieldRepository.FindAsync(id);
            customField.Status = FieldStatus.Hidden;
            _customFieldRepository.Update(customField);
            await _customFieldRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
