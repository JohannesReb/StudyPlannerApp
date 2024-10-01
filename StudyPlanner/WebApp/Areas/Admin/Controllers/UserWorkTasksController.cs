using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserWorkTasksController : Controller
    {
        private readonly AppDbContext _context;

        public UserWorkTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserWorkTasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserWorkTasks.Include(u => u.User).Include(u => u.WorkTask);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserWorkTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkTask = await _context.UserWorkTasks
                .Include(u => u.User)
                .Include(u => u.WorkTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userWorkTask == null)
            {
                return NotFound();
            }

            return View(userWorkTask);
        }

        // GET: UserWorkTasks/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label");
            return View();
        }

        // POST: UserWorkTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeSpent,CompletedAt,Result,Status,UserId,WorkTaskId,Id")] UserWorkTask userWorkTask)
        {
            if (ModelState.IsValid)
            {
                userWorkTask.Id = Guid.NewGuid();
                _context.Add(userWorkTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userWorkTask.UserId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", userWorkTask.WorkTaskId);
            return View(userWorkTask);
        }

        // GET: UserWorkTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkTask = await _context.UserWorkTasks.FindAsync(id);
            if (userWorkTask == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userWorkTask.UserId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", userWorkTask.WorkTaskId);
            return View(userWorkTask);
        }

        // POST: UserWorkTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TimeSpent,CompletedAt,Result,Status,UserId,WorkTaskId,Id")] UserWorkTask userWorkTask)
        {
            if (id != userWorkTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userWorkTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserWorkTaskExists(userWorkTask.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userWorkTask.UserId);
            ViewData["WorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", userWorkTask.WorkTaskId);
            return View(userWorkTask);
        }

        // GET: UserWorkTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkTask = await _context.UserWorkTasks
                .Include(u => u.User)
                .Include(u => u.WorkTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userWorkTask == null)
            {
                return NotFound();
            }

            return View(userWorkTask);
        }

        // POST: UserWorkTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userWorkTask = await _context.UserWorkTasks.FindAsync(id);
            if (userWorkTask != null)
            {
                _context.UserWorkTasks.Remove(userWorkTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserWorkTaskExists(Guid id)
        {
            return _context.UserWorkTasks.Any(e => e.Id == id);
        }
    }
}
