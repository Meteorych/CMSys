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
    public class TrainersListController : Controller
    {
        private readonly AppContext _context;

        public TrainersListController(AppContext context)
        {
            _context = context;
        }

        // GET: TrainersList
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Trainers.Include(t => t.TrainerGroup).Include(t => t.User);
            return View(await appContext.ToListAsync());
        }

        // GET: TrainersList/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .Include(t => t.TrainerGroup)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // GET: TrainersList/Create
        public IActionResult Create()
        {
            ViewData["TrainerGroupId"] = new SelectList(_context.TrainersGroups, "Id", "Name");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: TrainersList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisualOrder,TrainerGroupId,Description,Id")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                trainer.Id = Guid.NewGuid();
                _context.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerGroupId"] = new SelectList(_context.TrainersGroups, "Id", "Name", trainer.TrainerGroupId);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Email", trainer.Id);
            return View(trainer);
        }

        // GET: TrainersList/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            ViewData["TrainerGroupId"] = new SelectList(_context.TrainersGroups, "Id", "Name", trainer.TrainerGroupId);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Email", trainer.Id);
            return View(trainer);
        }

        // POST: TrainersList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VisualOrder,TrainerGroupId,Description,Id")] Trainer trainer)
        {
            if (id != trainer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.Id))
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
            ViewData["TrainerGroupId"] = new SelectList(_context.TrainersGroups, "Id", "Name", trainer.TrainerGroupId);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Email", trainer.Id);
            return View(trainer);
        }

        // GET: TrainersList/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .Include(t => t.TrainerGroup)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // POST: TrainersList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerExists(Guid id)
        {
            return _context.Trainers.Any(e => e.Id == id);
        }
    }
}
