using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using FormFactory;
using Interfaces.Repositories;
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

        // GET: ProjectTask/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(projectTask);
        }

        // GET: ProjectTask/Create
        public async Task<IActionResult> Create()
        {
            var customFields = await _customFieldRepository.AllWithReferencesAsync();
            var vm = new ProjectTaskViewModel
            {
                PropertyVms = MakeCustomFields(customFields)
            };

            return View(vm);
        }

        // POST: ProjectTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTask projectTask)
        {
            if (!ModelState.IsValid) return View(projectTask);

            _projectTaskRepository.Add(projectTask);
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
            return View(projectTask);
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

            if (!ModelState.IsValid) return View(projectTask);

            try
            {
                _projectTaskRepository.Update(projectTask);
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

            return View(projectTask);
        }

        public PropertyVm[] MakeCustomFields(List<CustomField> customFields)
        {
            return customFields.Select(customField => new PropertyVm(typeof(string), customField.FieldName)
            {
                DisplayName = customField.FieldName,
                NotOptional = customField.IsRequired
            }).ToArray();
        }
    }
}
