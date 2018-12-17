﻿using System.Linq;
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
            var customFields2 = customFields.Select(field => (ICustomField)field).ToList();
            var vm = new ProjectTaskViewModel
            {
                PropertyVms = CustomFields.Helpers.FormFieldHelper.MakeCustomFields<ProjectTask>(customFields2, false, nameof(ProjectTask.CustomFields))
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

            var customFields = projectTask.CustomFields.Select(combined => combined.CustomField).ToList();
            var customFields2 = customFields.Select(field => (ICustomField)field).ToList();
            var vm = new ProjectTaskViewModel
            {
                ProjectTask = projectTask,
                PropertyVms = CustomFields.Helpers.FormFieldHelper.MakeCustomFields<ProjectTask>
                    (customFields2, true, nameof(ProjectTask.CustomFields), id.Value)
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
                var customFields2 = taskFields.Select(field => (ICustomField)field).ToList();
                var vm = new ProjectTaskViewModel
                {
                    ProjectTask = projectTask,
                    PropertyVms = CustomFields.Helpers.FormFieldHelper.MakeCustomFields<ProjectTask>
                    (customFields2.Select(field => field).ToList(), true, nameof(ProjectTask.CustomFields), id)
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
