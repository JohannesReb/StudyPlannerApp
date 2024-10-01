using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserCurriculumsController : Controller
    {
        private readonly AppDbContext _context;

        public UserCurriculumsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserCurriculums
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserCurriculums.Include(u => u.Curriculum).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserCurriculums/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCurriculum = await _context.UserCurriculums
                .Include(u => u.Curriculum)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCurriculum == null)
            {
                return NotFound();
            }

            return View(userCurriculum);
        }

        // GET: UserCurriculums/Create
        public IActionResult Create()
        {
            ViewData["CurriculumId"] = new SelectList(_context.Curriculums, "Id", "Code");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserCurriculums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status,UserId,CurriculumId,Id")] UserCurriculum userCurriculum)
        {
            if (ModelState.IsValid)
            {
                userCurriculum.Id = Guid.NewGuid();
                _context.Add(userCurriculum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurriculumId"] = new SelectList(_context.Curriculums, "Id", "Code", userCurriculum.CurriculumId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userCurriculum.UserId);
            return View(userCurriculum);
        }

        // GET: UserCurriculums/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCurriculum = await _context.UserCurriculums.FindAsync(id);
            if (userCurriculum == null)
            {
                return NotFound();
            }
            ViewData["CurriculumId"] = new SelectList(_context.Curriculums, "Id", "Code", userCurriculum.CurriculumId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userCurriculum.UserId);
            return View(userCurriculum);
        }

        // POST: UserCurriculums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Status,UserId,CurriculumId,Id")] UserCurriculum userCurriculum)
        {
            if (id != userCurriculum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCurriculum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCurriculumExists(userCurriculum.Id))
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
            ViewData["CurriculumId"] = new SelectList(_context.Curriculums, "Id", "Code", userCurriculum.CurriculumId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userCurriculum.UserId);
            return View(userCurriculum);
        }

        // GET: UserCurriculums/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCurriculum = await _context.UserCurriculums
                .Include(u => u.Curriculum)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCurriculum == null)
            {
                return NotFound();
            }

            return View(userCurriculum);
        }

        // POST: UserCurriculums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userCurriculum = await _context.UserCurriculums.FindAsync(id);
            if (userCurriculum != null)
            {
                _context.UserCurriculums.Remove(userCurriculum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCurriculumExists(Guid id)
        {
            return _context.UserCurriculums.Any(e => e.Id == id);
        }
    }
}
