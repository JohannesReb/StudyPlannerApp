using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkTaskTimeWindowsController : Controller
    {
        private readonly AppDbContext _context;

        public WorkTaskTimeWindowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: WorkTaskTimeWindows
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.WorkTaskTimeWindows.Include(w => w.TimeWindow).Include(w => w.WorkTask);
            return View(await appDbContext.ToListAsync());
        }

        // GET: WorkTaskTimeWindows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTaskTimeWindow = await _context.WorkTaskTimeWindows
                .Include(w => w.TimeWindow)
                .Include(w => w.WorkTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTaskTimeWindow == null)
            {
                return NotFound();
            }

            return View(workTaskTimeWindow);
        }

        // GET: WorkTaskTimeWindows/Create
        public IActionResult Create()
        {
            ViewData["TimeWindowId"] = new SelectList(_context.TimeWindows, "Id", "Id");
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label");
            return View();
        }

        // POST: WorkTaskTimeWindows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkTaskId,TimeWindowId,Id")] WorkTaskTimeWindow workTaskTimeWindow)
        {
            if (ModelState.IsValid)
            {
                workTaskTimeWindow.Id = Guid.NewGuid();
                _context.Add(workTaskTimeWindow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TimeWindowId"] = new SelectList(_context.TimeWindows, "Id", "Id", workTaskTimeWindow.TimeWindowId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTaskTimeWindow.WorkTaskId);
            return View(workTaskTimeWindow);
        }

        // GET: WorkTaskTimeWindows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTaskTimeWindow = await _context.WorkTaskTimeWindows.FindAsync(id);
            if (workTaskTimeWindow == null)
            {
                return NotFound();
            }
            ViewData["TimeWindowId"] = new SelectList(_context.TimeWindows, "Id", "Id", workTaskTimeWindow.TimeWindowId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTaskTimeWindow.WorkTaskId);
            return View(workTaskTimeWindow);
        }

        // POST: WorkTaskTimeWindows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkTaskId,TimeWindowId,Id")] WorkTaskTimeWindow workTaskTimeWindow)
        {
            if (id != workTaskTimeWindow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTaskTimeWindow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTaskTimeWindowExists(workTaskTimeWindow.Id))
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
            ViewData["TimeWindowId"] = new SelectList(_context.TimeWindows, "Id", "Id", workTaskTimeWindow.TimeWindowId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTaskTimeWindow.WorkTaskId);
            return View(workTaskTimeWindow);
        }

        // GET: WorkTaskTimeWindows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTaskTimeWindow = await _context.WorkTaskTimeWindows
                .Include(w => w.TimeWindow)
                .Include(w => w.WorkTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTaskTimeWindow == null)
            {
                return NotFound();
            }

            return View(workTaskTimeWindow);
        }

        // POST: WorkTaskTimeWindows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workTaskTimeWindow = await _context.WorkTaskTimeWindows.FindAsync(id);
            if (workTaskTimeWindow != null)
            {
                _context.WorkTaskTimeWindows.Remove(workTaskTimeWindow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTaskTimeWindowExists(Guid id)
        {
            return _context.WorkTaskTimeWindows.Any(e => e.Id == id);
        }
    }
}
