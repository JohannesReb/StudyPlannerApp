using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EwentsController : Controller
    {
        private readonly AppDbContext _context;

        public EwentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ewents
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Ewents.Include(e => e.Subject);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Ewents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ewent = await _context.Ewents
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ewent == null)
            {
                return NotFound();
            }

            return View(ewent);
        }

        // GET: Ewents/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label");
            return View();
        }

        // POST: Ewents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Label,Description,From,Until,SubjectId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Ewent ewent)
        {
            if (ModelState.IsValid)
            {
                ewent.Id = Guid.NewGuid();
                _context.Add(ewent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", ewent.SubjectId);
            return View(ewent);
        }

        // GET: Ewents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ewent = await _context.Ewents.FindAsync(id);
            if (ewent == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", ewent.SubjectId);
            return View(ewent);
        }

        // POST: Ewents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Label,Description,From,Until,SubjectId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Ewent ewent)
        {
            if (id != ewent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ewent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EwentExists(ewent.Id))
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
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", ewent.SubjectId);
            return View(ewent);
        }

        // GET: Ewents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ewent = await _context.Ewents
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ewent == null)
            {
                return NotFound();
            }

            return View(ewent);
        }

        // POST: Ewents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ewent = await _context.Ewents.FindAsync(id);
            if (ewent != null)
            {
                _context.Ewents.Remove(ewent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EwentExists(Guid id)
        {
            return _context.Ewents.Any(e => e.Id == id);
        }
    }
}
