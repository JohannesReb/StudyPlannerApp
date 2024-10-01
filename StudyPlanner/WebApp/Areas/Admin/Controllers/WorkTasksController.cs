using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkTasksController : Controller
    {
        private readonly AppDbContext _context;

        public WorkTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: WorkTasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.WorkTasks.Include(w => w.ParentWorkTask).Include(w => w.Subject);
            return View(await appDbContext.ToListAsync());
        }

        // GET: WorkTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTasks
                .Include(w => w.ParentWorkTask)
                .Include(w => w.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }

        // GET: WorkTasks/Create
        public IActionResult Create()
        {
            ViewData["ParentWorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label");
            return View();
        }

        // POST: WorkTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Deadline,Label,TimeExpectancy,Code,MaxResult,TaskType,ParentWorkTaskId,SubjectId,Field,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] WorkTask workTask)
        {
            if (ModelState.IsValid)
            {
                workTask.Id = Guid.NewGuid();
                _context.Add(workTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentWorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTask.ParentWorkTaskId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", workTask.SubjectId);
            return View(workTask);
        }

        // GET: WorkTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }
            ViewData["ParentWorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTask.ParentWorkTaskId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", workTask.SubjectId);
            return View(workTask);
        }

        // POST: WorkTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Deadline,Label,TimeExpectancy,Code,MaxResult,TaskType,ParentWorkTaskId,SubjectId,Field,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] WorkTask workTask)
        {
            if (id != workTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTaskExists(workTask.Id))
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
            ViewData["ParentWorkTaskId"] = new SelectList(_context.WorkTasks, "Id", "Label", workTask.ParentWorkTaskId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", workTask.SubjectId);
            return View(workTask);
        }

        // GET: WorkTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTasks
                .Include(w => w.ParentWorkTask)
                .Include(w => w.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }

        // POST: WorkTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask != null)
            {
                _context.WorkTasks.Remove(workTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTaskExists(Guid id)
        {
            return _context.WorkTasks.Any(e => e.Id == id);
        }
    }
}
