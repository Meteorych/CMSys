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
    public class TrainerGroupsController : Controller
    {
        private readonly AppContext _context;

        public TrainerGroupsController(AppContext context)
        {
            _context = context;
        }

        // GET: TrainerGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainersGroups.ToListAsync());
        }

        // GET: TrainerGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerGroup = await _context.TrainersGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainerGroup == null)
            {
                return NotFound();
            }

            return View(trainerGroup);
        }

        // GET: TrainerGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainerGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,VisualOrder,Description,Id")] TrainerGroup trainerGroup)
        {
            if (ModelState.IsValid)
            {
                trainerGroup.Id = Guid.NewGuid();
                _context.Add(trainerGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainerGroup);
        }

        // GET: TrainerGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerGroup = await _context.TrainersGroups.FindAsync(id);
            if (trainerGroup == null)
            {
                return NotFound();
            }
            return View(trainerGroup);
        }

        // POST: TrainerGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,VisualOrder,Description,Id")] TrainerGroup trainerGroup)
        {
            if (id != trainerGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainerGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerGroupExists(trainerGroup.Id))
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
            return View(trainerGroup);
        }

        // GET: TrainerGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerGroup = await _context.TrainersGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainerGroup == null)
            {
                return NotFound();
            }

            return View(trainerGroup);
        }

        // POST: TrainerGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trainerGroup = await _context.TrainersGroups.FindAsync(id);
            if (trainerGroup != null)
            {
                _context.TrainersGroups.Remove(trainerGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerGroupExists(Guid id)
        {
            return _context.TrainersGroups.Any(e => e.Id == id);
        }
    }
}
