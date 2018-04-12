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
        public async Task<IActionResult> Index(bool hidden)
        {
            
            var fields = hidden
                ? await _customFieldRepository.AllIncludingHiddenAsync()
                : await _customFieldRepository.AllAsync();

            var vm = new CustomFieldIndexViewModel
            {
                Fields = fields.OrderBy(f => f.Sort),
                ShowHidden = hidden
            };

            return View(vm);
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
            return View(new CustomFieldCreateEditViewModel());
        }

        // POST: CustomFields/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomFieldCreateEditViewModel vm)
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

            var vm = new CustomFieldCreateEditViewModel
            {
                CustomField = customField,
                HasExistingData = customField.Tasks.Any()
            };

            return View(vm);
        }

        // POST: CustomFields/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomFieldCreateEditViewModel vm)
        {
            if (id != vm.CustomField.Id)
            {
                return NotFound();
            }

            var customField = _customFieldRepository.FindWithReferencesNoTracking(id);
            if (!ModelState.IsValid)
            {
                vm.HasExistingData = customField.Tasks.Any();
                return View(vm);
            }
            try
            {
                if (vm.CustomField.Status != FieldStatus.Disabled)
                {
                    vm.CustomField.Status = customField.Status;
                    _customFieldRepository.Update(vm.CustomField);
                }
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

            customField.Status = FieldStatus.Hidden;
            _customFieldRepository.Update(customField);
            await _customFieldRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomFields/Activate
        public async Task<IActionResult> Activate(int? id)
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

            customField.Status = FieldStatus.Active;
            _customFieldRepository.Update(customField);
            await _customFieldRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
