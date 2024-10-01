using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CurriculumsController : Controller
    {
        private readonly AppDbContext _context;

        public CurriculumsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Curriculums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Curriculums.ToListAsync());
        }

        // GET: Curriculums/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculum = await _context.Curriculums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curriculum == null)
            {
                return NotFound();
            }

            return View(curriculum);
        }

        // GET: Curriculums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curriculums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Label,ManagerName,From,Until,Semesters,Id")] Curriculum curriculum)
        {
            if (ModelState.IsValid)
            {
                curriculum.Id = Guid.NewGuid();
                _context.Add(curriculum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curriculum);
        }

        // GET: Curriculums/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculum = await _context.Curriculums.FindAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }
            return View(curriculum);
        }

        // POST: Curriculums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Code,Label,ManagerName,From,Until,Semesters,Id")] Curriculum curriculum)
        {
            if (id != curriculum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curriculum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurriculumExists(curriculum.Id))
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
            return View(curriculum);
        }

        // GET: Curriculums/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculum = await _context.Curriculums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curriculum == null)
            {
                return NotFound();
            }

            return View(curriculum);
        }

        // POST: Curriculums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var curriculum = await _context.Curriculums.FindAsync(id);
            if (curriculum != null)
            {
                _context.Curriculums.Remove(curriculum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurriculumExists(Guid id)
        {
            return _context.Curriculums.Any(e => e.Id == id);
        }
    }
}
