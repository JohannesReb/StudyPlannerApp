using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TimeWindowsController : Controller
    {
        private readonly AppDbContext _context;

        public TimeWindowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TimeWindows
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TimeWindows.Include(t => t.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TimeWindows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeWindow = await _context.TimeWindows
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeWindow == null)
            {
                return NotFound();
            }

            return View(timeWindow);
        }

        // GET: TimeWindows/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: TimeWindows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,Until,UserId,Id")] TimeWindow timeWindow)
        {
            if (ModelState.IsValid)
            {
                timeWindow.Id = Guid.NewGuid();
                _context.Add(timeWindow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", timeWindow.UserId);
            return View(timeWindow);
        }

        // GET: TimeWindows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeWindow = await _context.TimeWindows.FindAsync(id);
            if (timeWindow == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", timeWindow.UserId);
            return View(timeWindow);
        }

        // POST: TimeWindows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("From,Until,UserId,Id")] TimeWindow timeWindow)
        {
            if (id != timeWindow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeWindow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeWindowExists(timeWindow.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", timeWindow.UserId);
            return View(timeWindow);
        }

        // GET: TimeWindows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeWindow = await _context.TimeWindows
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeWindow == null)
            {
                return NotFound();
            }

            return View(timeWindow);
        }

        // POST: TimeWindows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var timeWindow = await _context.TimeWindows.FindAsync(id);
            if (timeWindow != null)
            {
                _context.TimeWindows.Remove(timeWindow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeWindowExists(Guid id)
        {
            return _context.TimeWindows.Any(e => e.Id == id);
        }
    }
}
