using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;
using App.Contracts.BLL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp.Areas.Public.ViewModels;

namespace WebApp.Areas.Public.Controllers
{
    [Area("Public")]
    [Authorize]
    public class CurriculumController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;

        public CurriculumController(IAppBLL bll, UserManager<User> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Curriculums
        public async Task<IActionResult> Index(Guid? curriculumId, Guid? moduleId)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new CurriculumIndexViewModel
            {
                Curriculums = await _bll.Curriculums.GetAllSortedAsync(userId),
                UserId = userId,
                CurriculumId = curriculumId,
                ModuleId = moduleId
            };
            if (curriculumId != null)
            {
                vm.ChosenSubjects = await _bll.Subjects.GetAllChosenSortedOfCurriculumAsync(userId, curriculumId.Value);
                vm.PublicSubjects = await _bll.Subjects.GetAllPublicSortedOfCurriculumAsync(userId, curriculumId.Value);
                vm.Curriculum = vm.Curriculums.FirstOrDefault(s => s.Id.Equals(curriculumId.Value))!;
                vm.Modules = await _bll.Modules.GetAllSortedOfCurriculumAsync(curriculumId.Value);
                if (moduleId == null)
                {
                    vm.UserSubjects = (await _bll.UserSubjects.GetAllAsync(userId));
                    vm.Completed = vm.UserSubjects.Where(u => vm.ChosenSubjects
                            .Select(s => s.Id).Contains(u.SubjectId) && u.Status.Equals(EStatus.Completed))
                        .Select(u => u.Subject!.EAP).Sum() ?? 0;
                    vm.Missing = vm.Modules.Select(m => m.EAP).Sum() - vm.Completed;
                    vm.Declared = vm.UserSubjects.Where(u => vm.ChosenSubjects
                            .Select(s => s.Id).Contains(u.SubjectId) && !u.Status.Equals(EStatus.Completed))
                        .Select(u => u.Subject!.EAP).Sum() ?? 0;
                    vm.NotDeclared = vm.Missing - vm.Declared;
                }
            }
            if (moduleId != null)
            {
                vm.PublicSubjects = vm.PublicSubjects.Where(s => s.ModuleId.Equals(moduleId));
                vm.ChosenSubjects = vm.ChosenSubjects.Where(s => s.ModuleId.Equals(moduleId));
                vm.Module = vm.Modules.FirstOrDefault(s => s.Id.Equals(moduleId.Value))!;
                vm.UserSubjects = (await _bll.UserSubjects.GetAllAsync(userId)).Where(u => u.Subject!.ModuleId.Equals(moduleId)).ToList();
                vm.Completed = vm.UserSubjects.Where(u => vm.ChosenSubjects
                        .Select(s => s.Id).Contains(u.SubjectId) && u.Status.Equals(EStatus.Completed))
                    .Select(u => u.Subject!.EAP).Sum() ?? 0;
                vm.Missing = vm.Module.EAP - vm.Completed;
                vm.Declared = vm.UserSubjects.Where(u => vm.ChosenSubjects
                        .Select(s => s.Id).Contains(u.SubjectId) && !u.Status.Equals(EStatus.Completed))
                    .Select(u => u.Subject!.EAP).Sum() ?? 0;
                vm.NotDeclared = vm.Module.EAP - vm.Declared;
            }
            
            return View(vm);
        }

        // GET: Curriculums/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new CurriculumDetailsDeleteViewModel()
            {
                Curriculum = await _bll.Curriculums.FirstOrDefaultAsync(id.Value),
                
            };
            if (vm.Curriculum == null)
            {
                return NotFound();
            }

            return View(vm);
        }
        
        
        
        
        // GET: Curriculums/CreateModule
        public async Task<IActionResult> CreateModule(Guid curriculumId)
        {
            var vm = new CurriculumCreateEditModuleViewModel()
            {
                Module = new Module()
                {
                    CurriculumId = curriculumId
                }
            };


            return View(vm);
        }

        // POST: Curriculums/CreateModule
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModule(CurriculumCreateEditModuleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Module.Id = Guid.NewGuid();
                _bll.Modules.Add(vm.Module);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {curriculumId = vm.Module.CurriculumId});
            }
            return View(vm);
        }
        
        
        // GET: Curriculums/Create
        public IActionResult CreateCurriculum()
        {
            return View(new CurriculumCreateEditCurriculumViewModel());
        }

        // POST: Curriculums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCurriculum(CurriculumCreateEditCurriculumViewModel vm)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            if (ModelState.IsValid)
            {
                vm.Curriculum.Id = Guid.NewGuid();
                _bll.Curriculums.Add(vm.Curriculum);
                await _bll.SaveChangesAsync();
                
                _bll.UserCurriculums.Add(new UserCurriculum()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = EStatus.Claimed,
                    CurriculumId = vm.Curriculum.Id
                });
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        
        // GET: Curriculums/Edit/5
        public async Task<IActionResult> EditModule(Guid? id, Guid? curriculumId)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var module = await _bll.Modules.FirstOrDefaultAsync(id.Value, userId);
            if (module == null)
            {
                return NotFound();
            }
            var vm = new CurriculumCreateEditModuleViewModel()
            {
                Module = module,
                CurriculumId = curriculumId
            };
            

            return View(vm);
        }

        // POST: Curriculums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModule(Guid id, CurriculumCreateEditModuleViewModel vm)
        {
            if (id != vm.Module.Id)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Modules.Update(vm.Module);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.Modules.ExistsAsync(vm.Module.Id, userId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {curriculumId = vm.CurriculumId});
            }
            return View(vm);
        }

        
        // GET: Curriculums/Edit/5
        public async Task<IActionResult> EditCurriculum(Guid? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var curriculum = await _bll.Curriculums.FirstOrDefaultAsync(id.Value, userId);
            if (curriculum == null)
            {
                return NotFound();
            }
            var vm = new CurriculumCreateEditCurriculumViewModel()
            {
                Curriculum = curriculum
            };

            return View(vm);
        }

        // POST: Curriculums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCurriculum(Guid id, CurriculumCreateEditCurriculumViewModel vm)
        {
            if (id != vm.Curriculum.Id)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Curriculums.Update(vm.Curriculum);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.Curriculums.ExistsAsync(vm.Curriculum.Id, userId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {curriculumId = vm.Curriculum.Id});
            }
            
            return View(vm);
        }

        
        // GET: Curriculums/Delete/5
        public async Task<IActionResult> DeleteModule(Guid? id, Guid? curriculumId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new CurriculumDeleteModuleViewModel()
            {
                Module = await _bll.Modules.FirstOrDefaultAsync(id.Value, userId),
                CurriculumId = curriculumId
                
            };
            
            if (vm.Module == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        
        // GET: Curriculums/Delete/5
        public async Task<IActionResult> DeleteCurriculum(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new CurriculumDetailsDeleteViewModel()
            {
                Curriculum = await _bll.Curriculums.FirstOrDefaultAsync(id.Value),
                
            };
            if (vm.Curriculum == null)
            {
                return NotFound();
            }

            return View(vm);
        }
        // POST: Curriculums/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteModule(Guid id, Guid curriculumId)
        {
            await _bll.Modules.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {curriculumId});
        }
        
        // POST: Curriculums/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCurriculum(Guid id, Guid curriculumId)
        {
            // if (!(await _bll.UserCurriculums.GetAllAsync()).IsNullOrEmpty())
            // {
            //     return RedirectToAction(nameof(Index), new {curriculumId}); // TODO: Error message "Users are using"
            // }
            await _bll.Curriculums.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {curriculumId});
        }
        
        // GET: TimeTable/Add/5
        public async Task<IActionResult> Add(Guid? id, Guid curriculumId, Guid moduleId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var subject = await _bll.Subjects.FirstOrDefaultAsync(id.Value, userId);
            if (subject == null)
            {
                return NotFound();
            }

            _bll.UserSubjects.Add(new UserSubject()
            {
                Id = Guid.NewGuid(),
                Status = EStatus.Claimed,
                SubjectId = subject.Id,
                UserId = userId
            });
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {curriculumId, moduleId});
        }
        
        // GET: TimeTable/Remove/5
        public async Task<IActionResult> Remove(Guid? id, Guid curriculumId, Guid moduleId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var subject = await _bll.Subjects.FirstOrDefaultAsync(id.Value, userId);
            if (subject == null)
            {
                return NotFound();
            }
            
            await _bll.UserSubjects.RemoveAsync(_bll.UserSubjects.GetFirstOfSubjectAsync(id.Value, userId).Result.Id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {curriculumId, moduleId});
        }
        
        // GET: TimeTable/Add/5
        public async Task<IActionResult> Choose(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var curriculum = await _bll.Curriculums.FirstOrDefaultAsync(id.Value, userId);
            if (curriculum == null)
            {
                return NotFound();
            }

            _bll.UserCurriculums.Add(new UserCurriculum()
            {
                Id = Guid.NewGuid(),
                Status = EStatus.Claimed,
                CurriculumId = curriculum.Id,
                UserId = userId
            });
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {curriculumId = id});
        }
        
        
        // GET: TimeTable/Remove/5
        public async Task<IActionResult> UnChoose(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var curriculum = await _bll.Curriculums.FirstOrDefaultAsync(id.Value, userId);
            if (curriculum == null)
            {
                return NotFound();
            }
            
            await _bll.UserCurriculums.RemoveAsync((await _bll.UserCurriculums.GetAllAsync()).First(u => u.CurriculumId.Equals(id) && u.UserId.Equals(userId)));
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {curriculumId = id});
        }
        
        public async Task<IActionResult> Start(Guid? id, Guid curriculumId, Guid moduleId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var userSubject = await _bll.UserSubjects.GetFirstOfSubjectAsync(id.Value, userId);
            userSubject.Status = EStatus.Pending;
            userSubject.User = null;
            userSubject.Subject = null;
            _bll.UserSubjects.Update(userSubject);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {curriculumId, moduleId});
        }
        public async Task<IActionResult> Finish(Guid? id, Guid curriculumId, Guid moduleId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var userSubject = await _bll.UserSubjects.GetFirstOfSubjectAsync(id.Value, userId);
            userSubject.Status = EStatus.Completed;
            userSubject.User = null;
            userSubject.Subject = null;
            _bll.UserSubjects.Update(userSubject);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {curriculumId, moduleId});
        }
    }
}
