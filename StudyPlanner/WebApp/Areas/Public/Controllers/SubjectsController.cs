using App.BLL.DTO.ManyToMany;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.BLL.DTO.Entities;
using App.BLL.DTO.Identity;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using User = App.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;
using WebApp.Areas.Public.ViewModels;

namespace WebApp.Areas.Public.Controllers
{
    [Area("Public")]
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;

        public SubjectsController(IAppBLL bll, UserManager<User> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Subjects
        public async Task<IActionResult> Index(Guid? subjectId)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new SubjectsIndexViewModel
            {
                Subjects = await _bll.Subjects.GetAllChosenSortedAsync(userId),
                WorkTaskRoles = await _bll.WorkTaskRoles.GetAllAsync(userId),
                UserId = userId,
                SubjectId = subjectId
            };
            if (subjectId != null)
            {
                vm.SubjectAccessTypes = (await _bll.SubjectRoles.GetAllOfSubjectAsync((Guid)subjectId, userId))
                    .Select(w => w.AccessType).ToList();
                vm.ChosenWorkTasks = await _bll.WorkTasks.GetAllChosenSortedOfSubjectAsync(userId, subjectId.Value);
                vm.PublicWorkTasks = await _bll.WorkTasks.GetAllPublicSortedOfSubjectAsync(userId, subjectId.Value);
                vm.Subject = vm.Subjects.FirstOrDefault(s => s.Id.Equals(subjectId.Value));
            }
            else
            {
                vm.ChosenWorkTasks = await _bll.WorkTasks.GetAllChosenSortedAsync(userId);
                vm.PublicWorkTasks = await _bll.WorkTasks.GetAllPublicSortedAsync(userId);
                
            }
            
            return View(vm);
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> SubjectDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new SubjectsSubjectDetailsDeleteViewModel()
            {
                Subject = await _bll.Subjects.FirstOrDefaultAsync(id.Value, userId),
                UserSubjects = (await _bll.UserSubjects.GetAllOfSubjectAsync(id.Value, userId)).ToList(),
                SubjectRoles = (await _bll.SubjectRoles.GetAllOfSubjectAsync(id.Value, userId)).ToList()
            };
            if (vm.Subject == null)
            {
                return NotFound();
            }

            return View(vm);
        }
        // GET: WorkTasks/Details/5
        public async Task<IActionResult> WorkTaskDetails(Guid? id, Guid? subjectId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new SubjectsTaskDetailsDeleteViewModel()
            {
                WorkTask = await _bll.WorkTasks.FirstOrDefaultAsync(id.Value, userId),
                UserWorkTasks = (await _bll.UserWorkTasks.GetAllOfWorkTaskAsync(id.Value, userId)).ToList(),
                WorkTaskRoles = (await _bll.WorkTaskRoles.GetAllOfWorkTaskAsync(id.Value, userId)).ToList(),
                SubjectId = subjectId
                
            };
            
            if (vm.WorkTask == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: Subjects/CreateWorkTask
        public async Task<IActionResult> CreateWorkTask(Guid subjectId)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new SubjectsCreateEditTaskViewModel()
            {
                WorkTask = new WorkTask()
                {
                    SubjectId = subjectId
                },
                ParentWorkTaskSelectList = new SelectList(await _bll.WorkTasks.GetAllAsync(userId), nameof(WorkTask.Id),
                    nameof(WorkTask.Label)),
                WorkTaskCodeSelectList = new SelectList(await _bll.WorkTasks.GetAllAsync(userId), nameof(WorkTask.Id),
                    nameof(WorkTask.Code)),
                RoleSelectList = new MultiSelectList(await _bll.Roles.GetAllAsync(userId), nameof(Role.Id),
                    nameof(Role.Name))
            };


            return View(vm);
        }

        // POST: Subjects/CreateWorkTask
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWorkTask(SubjectsCreateEditTaskViewModel vm)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            ModelState.ClearValidationState("Roles");
            ModelState.MarkFieldValid("Roles");
            if (ModelState.IsValid)
            {
                vm.WorkTask.Id = Guid.NewGuid();
                vm.WorkTask.CreatedBy = userId;
                vm.WorkTask.CreatedAt = DateTime.Now;
                _bll.WorkTasks.Add(vm.WorkTask);
                await _bll.SaveChangesAsync();
                foreach (var role in vm.Roles ?? [])
                {
                    _bll.WorkTaskRoles.Add(new WorkTaskRole()
                    {
                        Id = Guid.NewGuid(),
                        AccessType = vm.AccessType,
                        RoleId = role,
                        WorkTaskId = vm.WorkTask.Id
                    });
                }
                
                _bll.UserWorkTasks.Add(new UserWorkTask()
                {
                    Id = Guid.NewGuid(),
                    Status = EStatus.Claimed,
                    UserId = userId,
                    WorkTaskId = vm.WorkTask.Id
                });
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {subjectId = vm.WorkTask.SubjectId});
            }
            
            
            vm.ParentWorkTaskSelectList = new SelectList(await _bll.WorkTasks.GetAllAsync(userId), nameof(WorkTask.Id),
                nameof(WorkTask.Label), vm.WorkTask.ParentWorkTaskId);
            vm.RoleSelectList = new MultiSelectList(await _bll.Roles.GetAllAsync(userId),
                nameof(Role.Id), nameof(Role.Name), vm.Roles.ToArray());
            return View(vm);
        }
        
        
        // GET: Subjects/Create
        public async Task<IActionResult> CreateSubject()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new SubjectsCreateEditSubjectViewModel()
            {
                ModuleSelectList = new SelectList(await _bll.Modules.GetAllAsync(userId), nameof(Module.Id),
                    nameof(Module.Label)),
                RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(userId), nameof(Role.Id),
                    nameof(Role.Name))
            };


            return View(vm);
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject(SubjectsCreateEditSubjectViewModel vm)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            ModelState.ClearValidationState("Roles");
            ModelState.MarkFieldValid("Roles");
            if (ModelState.IsValid)
            {
                vm.Subject.Id = Guid.NewGuid();
                vm.Subject.CreatedBy = userId;
                vm.Subject.CreatedAt = DateTime.Now;
                _bll.Subjects.Add(vm.Subject);
                await _bll.SaveChangesAsync();
                foreach (var role in vm.Roles ?? [])
                {
                    _bll.SubjectRoles.Add(new SubjectRole()
                    {
                        Id = Guid.NewGuid(),
                        AccessType = vm.AccessType,
                        RoleId = role,
                        SubjectId = vm.Subject.Id
                    });
                }
                
                _bll.UserSubjects.Add(new UserSubject()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = EStatus.Claimed,
                    Semester = vm.Semester,
                    SubjectId = vm.Subject.Id
                });
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.ModuleSelectList = new SelectList(await _bll.Modules.GetAllAsync(userId), nameof(Module.Id), nameof(Module.Label), vm.Subject.ModuleId);
            vm.RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(userId), nameof(Role.Id), nameof(Role.Name), vm.Roles);
            return View(vm);
        }
        
        // GET: Subjects/Edit/5
        public async Task<IActionResult> EditWorkTask(Guid? id, Guid? subjectId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var workTask = await _bll.WorkTasks.FirstOrDefaultAsync(id.Value, userId);
            if (workTask == null)
            {
                return NotFound();
            }
            var workTaskRoles = (await _bll.WorkTaskRoles.GetAllOfWorkTaskAsync(workTask.Id, userId)).ToList();
            var vm = new SubjectsCreateEditTaskViewModel()
            {
                WorkTask = workTask,
                SubjectId = subjectId,
                AccessType = workTaskRoles.Select(w => w.AccessType).FirstOrDefault(),
                ParentWorkTaskSelectList = new SelectList(await _bll.WorkTasks.GetAllAsync(userId), nameof(WorkTask.Id),
                    nameof(WorkTask.Label), workTask.ParentWorkTaskId),
                WorkTaskCodeSelectList = new SelectList(await _bll.WorkTasks.GetAllAsync(userId), nameof(WorkTask.Id),
                    nameof(WorkTask.Code), workTask.Code),
                RoleSelectList = new MultiSelectList(await _bll.Roles.GetAllAsync(userId), nameof(Role.Id),
                    nameof(Role.Name), workTaskRoles.Select(w => w.RoleId)),
                AccessTypes = (await _bll.WorkTaskRoles.GetAllAsync(userId)).Where(w => w.WorkTaskId == workTask.Id).Select(w => w.AccessType).ToList(),
                UserId = userId
            };
            

            return View(vm);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWorkTask(Guid id, SubjectsCreateEditTaskViewModel vm)
        {
            ModelState.ClearValidationState("Roles");
            ModelState.MarkFieldValid("Roles");
            if (id != vm.WorkTask.Id)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            if (ModelState.IsValid)
            {
                try
                {
                    vm.WorkTask.UpdatedBy = userId;
                    vm.WorkTask.UpdatedAt = DateTime.Now;
                    _bll.WorkTasks.Update(vm.WorkTask);
                    await _bll.SaveChangesAsync();
                    var workTaskRoles = (await _bll.WorkTaskRoles.GetAllOfWorkTaskAsync(vm.WorkTask.Id, userId)).ToList();
                    foreach (var role in vm.Roles ?? [])
                    {
                        var workTaskRole = workTaskRoles.Find(w => w.RoleId.Equals(role));
                        if (workTaskRole != default)
                        {
                            if (workTaskRole.AccessType != vm.AccessType)
                            {
                                workTaskRole.AccessType = vm.AccessType;
                                workTaskRole.WorkTask = null;
                                workTaskRole.Role = null;
                                _bll.WorkTaskRoles.Update(workTaskRole);
                            }
                        }
                        else
                        {
                            _bll.WorkTaskRoles.Add(new WorkTaskRole()
                            {
                                Id = Guid.NewGuid(),
                                AccessType = vm.AccessType,
                                RoleId = role,
                                WorkTaskId = vm.WorkTask.Id
                            });
                        }
                    }

                    foreach (var roleId in workTaskRoles.Select(w => w.RoleId))
                    {
                        if (!(vm.Roles ?? []).ToList().Exists(r => r.Equals(roleId)))
                        {
                            var workTaskRole = workTaskRoles.Find(w => w.RoleId.Equals(roleId) && vm.WorkTask.Id.Equals(w.WorkTaskId));
                            await _bll.WorkTaskRoles.RemoveAsync(workTaskRole!);
                        }
                    }
                    
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.WorkTasks.ExistsAsync(vm.WorkTask.Id, userId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {subjectId = vm.SubjectId});
            }
            
            vm.ParentWorkTaskSelectList = new SelectList(await _bll.WorkTasks.GetAllAsync(userId), nameof(WorkTask.Id),
                nameof(WorkTask.Label), vm.WorkTask.ParentWorkTaskId);
            vm.RoleSelectList = new MultiSelectList(await _bll.Roles.GetAllAsync(userId),
                nameof(Role.Id), nameof(Role.Name), vm.Roles);
            vm.AccessTypes = (await _bll.WorkTaskRoles.GetAllAsync(userId)).Where(w => w.WorkTaskId == vm.WorkTask.Id).Select(w => w.AccessType).ToList();
            //vm.UserId = userId;

            return View(vm);
        }

        
        // GET: Subjects/Edit/5
        public async Task<IActionResult> EditSubject(Guid? id)
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
            var subjectRoles = (await _bll.SubjectRoles.GetAllOfSubjectAsync(subject.Id, userId));
            var vm = new SubjectsCreateEditSubjectViewModel()
            {
                Subject = subject,
                ModuleSelectList = new SelectList(await _bll.Modules.GetAllAsync(userId), nameof(Module.Id),
                nameof(Module.Label), subject.ModuleId),
                RoleSelectList = new MultiSelectList(await _bll.Roles.GetAllAsync(userId), nameof(Role.Id),
                    nameof(Role.Name), subjectRoles.Select(s => s.RoleId)),
                AccessTypes = (await _bll.SubjectRoles.GetAllAsync(userId)).Where(w => w.SubjectId == subject.Id).Select(w => w.AccessType).ToList(),
                UserId = userId
            };

            return View(vm);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(Guid id, SubjectsCreateEditSubjectViewModel vm)
        {
            ModelState.ClearValidationState("Roles");
            ModelState.MarkFieldValid("Roles");
            if (id != vm.Subject.Id)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            if (ModelState.IsValid)
            {
                try
                {
                    vm.Subject.UpdatedBy = userId;
                    vm.Subject.UpdatedAt = DateTime.Now;
                    _bll.Subjects.Update(vm.Subject);
                    await _bll.SaveChangesAsync();
                    var subjectRoles = (await _bll.SubjectRoles.GetAllOfSubjectAsync(vm.Subject.Id, userId)).ToList();
                    foreach (var role in vm.Roles ?? [])
                    {
                        if (subjectRoles.Exists(w => w.RoleId.Equals(role)))
                        {
                            var subjectRole = subjectRoles.Find(w => w.RoleId.Equals(role) && vm.Subject.Id.Equals(w.SubjectId));
                            if (subjectRole!.AccessType != vm.AccessType)
                            {
                                subjectRole.AccessType = vm.AccessType;
                                subjectRole.Subject = null;
                                subjectRole.Role = null;
                                _bll.SubjectRoles.Update(subjectRole);
                            }
                        }
                        else
                        {
                            _bll.SubjectRoles.Add(new SubjectRole()
                            {
                                Id = Guid.NewGuid(),
                                AccessType = vm.AccessType,
                                RoleId = role,
                                SubjectId = vm.Subject.Id
                            });
                        }
                    }

                    foreach (var roleId in subjectRoles.Select(w => w.RoleId))
                    {
                        if (!(vm.Roles ?? []).ToList().Exists(r => r.Equals(roleId)))
                        {
                            var subjectRole = subjectRoles.Find(w => w.RoleId.Equals(roleId) && vm.Subject.Id.Equals(w.SubjectId));
                            await _bll.SubjectRoles.RemoveAsync(subjectRole!);
                        }
                    }
                    
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.Subjects.ExistsAsync(vm.Subject.Id, userId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {subjectId = vm.Subject.Id});
            }
            
            vm.ModuleSelectList = new SelectList(await _bll.Modules.GetAllAsync(userId),
                nameof(Module.Id), nameof(Module.Label), vm.Subject.ModuleId);
            vm.RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(userId),
                nameof(Role.Id), nameof(Role.Name), vm.Roles);
            vm.AccessTypes = (await _bll.SubjectRoles.GetAllAsync(userId)).Where(w => w.SubjectId == vm.Subject.Id)
                .Select(w => w.AccessType).ToList();
            
            return View(vm);
        }

        
        // GET: Subjects/Delete/5
        public async Task<IActionResult> DeleteWorkTask(Guid? id, Guid? subjectId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new SubjectsTaskDetailsDeleteViewModel()
            {
                WorkTask = await _bll.WorkTasks.FirstOrDefaultAsync(id.Value, userId),
                UserWorkTasks = (await _bll.UserWorkTasks.GetAllOfWorkTaskAsync(id.Value, userId)).ToList(),
                WorkTaskRoles = (await _bll.WorkTaskRoles.GetAllOfWorkTaskAsync(id.Value, userId)).ToList(),
                SubjectId = subjectId
                
            };
            
            if (vm.WorkTask == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        
        // GET: Subjects/Delete/5
        public async Task<IActionResult> DeleteSubject(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new SubjectsSubjectDetailsDeleteViewModel()
            {
                Subject = await _bll.Subjects.FirstOrDefaultAsync(id.Value, userId),
                UserSubjects = (await _bll.UserSubjects.GetAllOfSubjectAsync(id.Value, userId)).ToList(),
                SubjectRoles = (await _bll.SubjectRoles.GetAllOfSubjectAsync(id.Value, userId)).ToList()
                
            };
            if (vm.Subject == null)
            {
                return NotFound();
            }

            return View(vm);
        }
        // POST: Subjects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkTask(Guid id, Guid? subjectId)
        {
            await _bll.WorkTasks.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {subjectId});
        }
        
        // POST: Subjects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubject(Guid id, Guid? subjectId)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            await _bll.Subjects.RemoveAsync(id, userId);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {subjectId});
        }
        
        // GET: TimeTable/Add/5
        public async Task<IActionResult> Add(Guid? id, Guid? subjectId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var workTask = await _bll.WorkTasks.FirstOrDefaultAsync(id.Value, userId);
            if (workTask == null)
            {
                return NotFound();
            }
            
            _bll.UserWorkTasks.Add(new UserWorkTask()
            {
                Id = Guid.NewGuid(),
                Status = EStatus.Claimed,
                WorkTaskId = workTask.Id,
                UserId = userId
            });
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {subjectId});
        }
        
        // GET: TimeTable/Remove/5
        public async Task<IActionResult> Remove(Guid? id, Guid? subjectId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var workTask = await _bll.WorkTasks.FirstOrDefaultAsync(id.Value, userId);
            if (workTask == null)
            {
                return NotFound();
            }
            
            await _bll.UserWorkTasks.RemoveAsync(_bll.UserWorkTasks.GetFirstOfWorkTaskAsync(id.Value, userId).Result!.Id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {subjectId});
        }
    }
}
