using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubjectRolesController : Controller
    {
        private readonly AppDbContext _context;

        public SubjectRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SubjectRoles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SubjectRoles.Include(s => s.Role).Include(s => s.Subject);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SubjectRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectRole = await _context.SubjectRoles
                .Include(s => s.Role)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjectRole == null)
            {
                return NotFound();
            }

            return View(subjectRole);
        }

        // GET: SubjectRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label");
            return View();
        }

        // POST: SubjectRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,SubjectId,AccessType,Id")] SubjectRole subjectRole)
        {
            if (ModelState.IsValid)
            {
                subjectRole.Id = Guid.NewGuid();
                _context.Add(subjectRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", subjectRole.RoleId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", subjectRole.SubjectId);
            return View(subjectRole);
        }

        // GET: SubjectRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectRole = await _context.SubjectRoles.FindAsync(id);
            if (subjectRole == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", subjectRole.RoleId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", subjectRole.SubjectId);
            return View(subjectRole);
        }

        // POST: SubjectRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RoleId,SubjectId,AccessType,Id")] SubjectRole subjectRole)
        {
            if (id != subjectRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectRoleExists(subjectRole.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", subjectRole.RoleId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", subjectRole.SubjectId);
            return View(subjectRole);
        }

        // GET: SubjectRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectRole = await _context.SubjectRoles
                .Include(s => s.Role)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjectRole == null)
            {
                return NotFound();
            }

            return View(subjectRole);
        }

        // POST: SubjectRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var subjectRole = await _context.SubjectRoles.FindAsync(id);
            if (subjectRole != null)
            {
                _context.SubjectRoles.Remove(subjectRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectRoleExists(Guid id)
        {
            return _context.SubjectRoles.Any(e => e.Id == id);
        }
    }
}
