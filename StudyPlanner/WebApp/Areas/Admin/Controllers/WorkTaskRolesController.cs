using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkTaskRolesController : Controller
    {
        private readonly AppDbContext _context;

        public WorkTaskRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: WorkTaskRoles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.WorkTaskRoles.Include(w => w.Role).Include(w => w.WorkTask);
            return View(await appDbContext.ToListAsync());
        }

        // GET: WorkTaskRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTaskRole = await _context.WorkTaskRoles
                .Include(w => w.Role)
                .Include(w => w.WorkTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTaskRole == null)
            {
                return NotFound();
            }

            return View(workTaskRole);
        }

        // GET: WorkTaskRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label");
            return View();
        }

        // POST: WorkTaskRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,WorkTaskId,AccessType,Id")] WorkTaskRole workTaskRole)
        {
            if (ModelState.IsValid)
            {
                workTaskRole.Id = Guid.NewGuid();
                _context.Add(workTaskRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", workTaskRole.RoleId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTaskRole.WorkTaskId);
            return View(workTaskRole);
        }

        // GET: WorkTaskRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTaskRole = await _context.WorkTaskRoles.FindAsync(id);
            if (workTaskRole == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", workTaskRole.RoleId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTaskRole.WorkTaskId);
            return View(workTaskRole);
        }

        // POST: WorkTaskRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RoleId,WorkTaskId,AccessType,Id")] WorkTaskRole workTaskRole)
        {
            if (id != workTaskRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTaskRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTaskRoleExists(workTaskRole.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", workTaskRole.RoleId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTaskRole.WorkTaskId);
            return View(workTaskRole);
        }

        // GET: WorkTaskRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTaskRole = await _context.WorkTaskRoles
                .Include(w => w.Role)
                .Include(w => w.WorkTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTaskRole == null)
            {
                return NotFound();
            }

            return View(workTaskRole);
        }

        // POST: WorkTaskRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workTaskRole = await _context.WorkTaskRoles.FindAsync(id);
            if (workTaskRole != null)
            {
                _context.WorkTaskRoles.Remove(workTaskRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTaskRoleExists(Guid id)
        {
            return _context.WorkTaskRoles.Any(e => e.Id == id);
        }
    }
}
