using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMSys.Core.Entities.Catalog;
using CMSys.Infrastructure;
using AppContext = CMSys.Infrastructure.AppContext;
using Microsoft.AspNetCore.Authorization;

namespace CMSysRealization.Controllers
{
    [Authorize]
    public class CourseGroupsController : Controller
    {
        private readonly AppContext _context;

        public CourseGroupsController(AppContext context)
        {
            _context = context;
        }

        // GET: CourseGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoursesGroups.ToListAsync());
        }

        // GET: CourseGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseGroup = await _context.CoursesGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseGroup == null)
            {
                return NotFound();
            }

            return View(courseGroup);
        }

        // GET: CourseGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,VisualOrder,Description,Id")] CourseGroup courseGroup)
        {
            if (ModelState.IsValid)
            {
                courseGroup.Id = Guid.NewGuid();
                _context.Add(courseGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseGroup);
        }

        // GET: CourseGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseGroup = await _context.CoursesGroups.FindAsync(id);
            if (courseGroup == null)
            {
                return NotFound();
            }
            return View(courseGroup);
        }

        // POST: CourseGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,VisualOrder,Description,Id")] CourseGroup courseGroup)
        {
            if (id != courseGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseGroupExists(courseGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseGroup);
        }

        // GET: CourseGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseGroup = await _context.CoursesGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseGroup == null)
            {
                return NotFound();
            }

            return View(courseGroup);
        }

        // POST: CourseGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var courseGroup = await _context.CoursesGroups.FindAsync(id);
            if (courseGroup != null)
            {
                _context.CoursesGroups.Remove(courseGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseGroupExists(Guid id)
        {
            return _context.CoursesGroups.Any(e => e.Id == id);
        }
    }
}
