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

            var vm = new IndexViewModel
            {
                Fields = fields.OrderBy(f => f.Sort),
                ShowHidden = hidden
            };

            return View(nameof(Index), vm);
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

            return View(nameof(Details), customField);
        }

        // GET: CustomFields/Create
        public IActionResult Create()
        {
            return View(nameof(Create), new CreateEditViewModel());
        }

        // POST: CustomFields/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel vm)
        {
            if (!ModelState.IsValid) return View(nameof(Create), vm);

            _customFieldRepository.AddAsync(new CustomField2(vm.CustomField)).Wait();
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

            var vm = new CreateEditViewModel
            {
                CustomField = customField,
                HasExistingData = customField.CombinedFields.Any()
            };

            return View(nameof(Edit), vm);
        }

        // POST: CustomFields/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditViewModel vm)
        {
            if (id != vm.CustomField.Id)
            {
                return NotFound();
            }

            var customField = _customFieldRepository.FindWithReferencesNoTracking(id);
            if (!ModelState.IsValid)
            {
                vm.HasExistingData = customField.CombinedFields.Any();
                return View(nameof(Edit), vm);
            }
            try
            {
                vm.CustomField.Status = customField.Status;

                _customFieldRepository.Update(new CustomField2(vm.CustomField));
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
