using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserFieldsController : Controller
    {
        private readonly AppDbContext _context;

        public UserFieldsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserFields
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserFields.Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserFields/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userField = await _context.UserFields
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userField == null)
            {
                return NotFound();
            }

            return View(userField);
        }

        // GET: UserFields/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserFields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Multiplier,UserId,Field,Id")] UserField userField)
        {
            if (ModelState.IsValid)
            {
                userField.Id = Guid.NewGuid();
                _context.Add(userField);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userField.UserId);
            return View(userField);
        }

        // GET: UserFields/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userField = await _context.UserFields.FindAsync(id);
            if (userField == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userField.UserId);
            return View(userField);
        }

        // POST: UserFields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Multiplier,UserId,Field,Id")] UserField userField)
        {
            if (id != userField.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFieldExists(userField.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userField.UserId);
            return View(userField);
        }

        // GET: UserFields/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userField = await _context.UserFields
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userField == null)
            {
                return NotFound();
            }

            return View(userField);
        }

        // POST: UserFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userField = await _context.UserFields.FindAsync(id);
            if (userField != null)
            {
                _context.UserFields.Remove(userField);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFieldExists(Guid id)
        {
            return _context.UserFields.Any(e => e.Id == id);
        }
    }
}
