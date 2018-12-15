using System.Linq;
using System.Threading.Tasks;
using CustomFields.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Interfaces.Repositories;
using CustomFields.ViewModels;
using Domain;

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
            return View(new CustomFieldCreateEditViewModel<CustomField2>());
        }

        // POST: CustomFields/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomFieldCreateEditViewModel<CustomField2> vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _customFieldRepository.Add(vm.CustomField2);
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

            var vm = new CustomFieldCreateEditViewModel<CustomField2>
            {
                CustomField2 = customField,
                HasExistingData = customField.CombinedFields.Any()
            };

            return View(vm);
        }

        // POST: CustomFields/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomFieldCreateEditViewModel<CustomField2> vm)
        {
            if (id != vm.CustomField2.Id)
            {
                return NotFound();
            }

            var customField = _customFieldRepository.FindWithReferencesNoTracking(id);
            if (!ModelState.IsValid)
            {
                vm.HasExistingData = customField.CombinedFields.Any();
                return View(vm);
            }
            try
            {
                vm.CustomField2.Status = customField.Status;
                _customFieldRepository.Update(vm.CustomField2);
                await _customFieldRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_customFieldRepository.Exists(vm.CustomField2.Id))
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
