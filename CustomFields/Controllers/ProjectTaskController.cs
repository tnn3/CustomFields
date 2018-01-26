using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories;
using Domain;
using Interfaces.Repositories;

namespace WebApplication.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public ProjectTaskController(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
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
        public IActionResult Create()
        {
            return View();
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
    }
}
