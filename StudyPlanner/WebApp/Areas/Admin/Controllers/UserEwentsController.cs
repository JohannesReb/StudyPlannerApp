using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserEwentsController : Controller
    {
        private readonly AppDbContext _context;

        public UserEwentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserEwents
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserEwents.Include(u => u.Ewent).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserEwents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEwent = await _context.UserEwents
                .Include(u => u.Ewent)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEwent == null)
            {
                return NotFound();
            }

            return View(userEwent);
        }

        // GET: UserEwents/Create
        public IActionResult Create()
        {
            ViewData["EwentId"] = new SelectList(_context.Ewents, "Id", "Label");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserEwents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,EwentId,Id")] UserEwent userEwent)
        {
            if (ModelState.IsValid)
            {
                userEwent.Id = Guid.NewGuid();
                _context.Add(userEwent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EwentId"] = new SelectList(_context.Ewents, "Id", "Label", userEwent.EwentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userEwent.UserId);
            return View(userEwent);
        }

        // GET: UserEwents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEwent = await _context.UserEwents.FindAsync(id);
            if (userEwent == null)
            {
                return NotFound();
            }
            ViewData["EwentId"] = new SelectList(_context.Ewents, "Id", "Label", userEwent.EwentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userEwent.UserId);
            return View(userEwent);
        }

        // POST: UserEwents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,EwentId,Id")] UserEwent userEwent)
        {
            if (id != userEwent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userEwent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserEwentExists(userEwent.Id))
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
            ViewData["EwentId"] = new SelectList(_context.Ewents, "Id", "Label", userEwent.EwentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userEwent.UserId);
            return View(userEwent);
        }

        // GET: UserEwents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEwent = await _context.UserEwents
                .Include(u => u.Ewent)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEwent == null)
            {
                return NotFound();
            }

            return View(userEwent);
        }

        // POST: UserEwents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userEwent = await _context.UserEwents.FindAsync(id);
            if (userEwent != null)
            {
                _context.UserEwents.Remove(userEwent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserEwentExists(Guid id)
        {
            return _context.UserEwents.Any(e => e.Id == id);
        }
    }
}
