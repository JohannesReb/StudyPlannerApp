using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserSubjectsController : Controller
    {
        private readonly AppDbContext _context;

        public UserSubjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserSubjects
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserSubjects.Include(u => u.Subject).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserSubjects/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSubject = await _context.UserSubjects
                .Include(u => u.Subject)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSubject == null)
            {
                return NotFound();
            }

            return View(userSubject);
        }

        // GET: UserSubjects/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Grade,Status,Semester,UserId,SubjectId,Id")] UserSubject userSubject)
        {
            if (ModelState.IsValid)
            {
                userSubject.Id = Guid.NewGuid();
                _context.Add(userSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", userSubject.SubjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userSubject.UserId);
            return View(userSubject);
        }

        // GET: UserSubjects/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSubject = await _context.UserSubjects.FindAsync(id);
            if (userSubject == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", userSubject.SubjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userSubject.UserId);
            return View(userSubject);
        }

        // POST: UserSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Grade,Status,Semester,UserId,SubjectId,Id")] UserSubject userSubject)
        {
            if (id != userSubject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSubjectExists(userSubject.Id))
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
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Label", userSubject.SubjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userSubject.UserId);
            return View(userSubject);
        }

        // GET: UserSubjects/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSubject = await _context.UserSubjects
                .Include(u => u.Subject)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSubject == null)
            {
                return NotFound();
            }

            return View(userSubject);
        }

        // POST: UserSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userSubject = await _context.UserSubjects.FindAsync(id);
            if (userSubject != null)
            {
                _context.UserSubjects.Remove(userSubject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSubjectExists(Guid id)
        {
            return _context.UserSubjects.Any(e => e.Id == id);
        }
    }
}
