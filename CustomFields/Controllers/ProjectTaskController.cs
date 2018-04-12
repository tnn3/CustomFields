using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Repositories;
using WebApplication.Helpers;
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
                PropertyVms = FormFieldHelper.MakeCustomFields<ProjectTask>(customFields, false)
            };

            return View(vm);
        }

        // POST: ProjectTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTaskViewModel vm){
            if (!ModelState.IsValid) return View(vm);

            _projectTaskRepository.Add(vm.ProjectTask);
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

            var customFields = await _customFieldRepository.AllWithReferencesAsync();
            var vm = new ProjectTaskViewModel
            {
                ProjectTask = projectTask,
                PropertyVms = FormFieldHelper.MakeCustomFields<ProjectTask>(customFields, true, id.Value)
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
            _projectTaskRepository.Remove(projectTask);
            await _projectTaskRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
