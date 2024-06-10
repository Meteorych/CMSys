
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMSys.Core.Entities.Catalog;
using CMSys.Infrastructure;
using AppContext = CMSys.Infrastructure.AppContext;

namespace CMSysRealization.Controllers
{
    public class TrainersController : Controller
    {
        private readonly AppContext _context;

        public TrainersController(AppContext context)
        {
            _context = context;
        }

        // GET: Trainers
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Trainers.Include(t => t.TrainerGroup).Include(t => t.User);
            return View(await appContext.ToListAsync());
        }

        // GET: Trainers/Details/5
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

        // GET: Trainers/Create
        public IActionResult Create()
        {
            ViewData["TrainerGroupId"] = new SelectList(_context.TrainersGroups, "Id", "Name");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Trainers/Create
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

        // GET: Trainers/Edit/5
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

        // POST: Trainers/Edit/5
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

        // GET: Trainers/Delete/5
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

        // POST: Trainers/Delete/5
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
