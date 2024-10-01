using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EwentRolesController : Controller
    {
        private readonly AppDbContext _context;

        public EwentRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EwentRoles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EwentRoles.Include(e => e.Ewent).Include(e => e.Role);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EwentRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ewentRole = await _context.EwentRoles
                .Include(e => e.Ewent)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ewentRole == null)
            {
                return NotFound();
            }

            return View(ewentRole);
        }

        // GET: EwentRoles/Create
        public IActionResult Create()
        {
            ViewData["EwentId"] = new SelectList(_context.Ewents, "Id", "Label");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }

        // POST: EwentRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,EwentId,AccessType,Id")] EwentRole ewentRole)
        {
            if (ModelState.IsValid)
            {
                ewentRole.Id = Guid.NewGuid();
                _context.Add(ewentRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EwentId"] = new SelectList(_context.Ewents, "Id", "Label", ewentRole.EwentId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", ewentRole.RoleId);
            return View(ewentRole);
        }

        // GET: EwentRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ewentRole = await _context.EwentRoles.FindAsync(id);
            if (ewentRole == null)
            {
                return NotFound();
            }
            ViewData["EwentId"] = new SelectList(_context.Ewents, "Id", "Label", ewentRole.EwentId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", ewentRole.RoleId);
            return View(ewentRole);
        }

        // POST: EwentRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RoleId,EwentId,AccessType,Id")] EwentRole ewentRole)
        {
            if (id != ewentRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ewentRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EwentRoleExists(ewentRole.Id))
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
            ViewData["EwentId"] = new SelectList(_context.Ewents, "Id", "Label", ewentRole.EwentId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", ewentRole.RoleId);
            return View(ewentRole);
        }

        // GET: EwentRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ewentRole = await _context.EwentRoles
                .Include(e => e.Ewent)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ewentRole == null)
            {
                return NotFound();
            }

            return View(ewentRole);
        }

        // POST: EwentRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ewentRole = await _context.EwentRoles.FindAsync(id);
            if (ewentRole != null)
            {
                _context.EwentRoles.Remove(ewentRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EwentRoleExists(Guid id)
        {
            return _context.EwentRoles.Any(e => e.Id == id);
        }
    }
}
