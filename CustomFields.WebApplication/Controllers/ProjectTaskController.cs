using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Repositories;
using CustomFields.Helpers;
using CustomFields.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly ICustomFieldRepository _customFieldRepository;

        public ProjectTaskController(IProjectTaskRepository projectTaskRepository, ICustomFieldRepository customFieldRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _customFieldRepository = customFieldRepository;
        }

        // GET: ProjectTask
        public async Task<IActionResult> Index()
        {
            return View(await _projectTaskRepository.AllAsyncWithReferences());
        }

        // GET: ProjectTask/Create
        public async Task<IActionResult> Create()
        {
            List<CustomFieldInProject> customFields = await _customFieldRepository.AllAsync();
            IEnumerable<ICustomField> customFieldInterfaces = customFields.Select(field => (ICustomField)field);
            var vm = new ProjectTaskViewModel
            {
                CustomFields = customFieldInterfaces
            };

            return View(vm);
        }

        // POST: ProjectTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTaskViewModel vm)
        {
            //TODO field validation
            if (!ModelState.IsValid)
            {
                List<CustomFieldInProject> customFields = await _customFieldRepository.AllWithReferencesAsync();
                IEnumerable<ICustomField> customFieldInterfaces = customFields.Select(field => (ICustomField)field);
                vm.CustomFields = customFieldInterfaces;
                return View(vm);
            }

            _projectTaskRepository.AddAsync(vm.ProjectTask).Wait();
            await _projectTaskRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProjectTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _projectTaskRepository.FindAsyncWithReferences(id.Value);
            if (projectTask == null)
            {
                return NotFound();
            }

            var customFields = projectTask.CustomFields.Select(combined => (ICustomField)combined.CustomField).ToList();
            var vm = new ProjectTaskViewModel
            {
                ProjectTask = projectTask,
                CustomFields = customFields
            };
            return View(vm);
        }

        // POST: ProjectTask/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTask projectTask)
        {
            if (id != projectTask.Id)
            {
                return NotFound();
            }

            var oldTask = await _projectTaskRepository.FindAsyncWithReferences(id);
            var taskFields = await _customFieldRepository.AllWithValuesByTaskId(id);

            foreach (var oldTaskField in oldTask.CustomFields)
            {
                foreach (var vmField in projectTask.CustomFields)
                {
                    if (vmField.CustomFieldId != oldTaskField.CustomFieldId) continue;

                    var taskField = taskFields.First(c => c.Id == oldTaskField.CustomFieldId);

                    var validationErrors = CustomFieldHelper.ValidateCustomField(taskField, vmField.FieldValue);
                    validationErrors.ForEach(error => ModelState.AddModelError(string.Empty, error));

                    oldTaskField.FieldValue = vmField.FieldValue;
                }
            }
            oldTask.Title = projectTask.Title;

            if (!ModelState.IsValid)
            {
                var customFields2 = taskFields.Select(field => (ICustomField)field);
                var vm = new ProjectTaskViewModel
                {
                    ProjectTask = projectTask,
                    CustomFields = customFields2
                };
                return View(vm);
            }

            try
            {
                _projectTaskRepository.Update(oldTask);
                await _projectTaskRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_projectTaskRepository.Exists(projectTask.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProjectTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _projectTaskRepository.FindAsync(id.Value);
            if (projectTask == null)
            {
                return NotFound();
            }
            _projectTaskRepository.Remove(projectTask);
            await _projectTaskRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
