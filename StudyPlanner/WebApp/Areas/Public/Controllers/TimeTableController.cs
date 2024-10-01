using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;
using App.Contracts.BLL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Public.ViewModels;

namespace WebApp.Areas.Public.Controllers
{
    [Area("Public")]
    [Authorize]
    public class TimeTableController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;

        public TimeTableController(IAppBLL bll, UserManager<User> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: TimeTable
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var vm = new TimeTableIndexViewModel()
            {
                WorkTasks = (await _bll.WorkTasks.GetAllChosenSortedAsync(userId)).ToList(),
                TimeWindows = (await _bll.TimeWindows.GetAllActiveSortedAsync(userId)).ToList(),
                WorkTaskTimeWindows = (await _bll.WorkTaskTimeWindows.GetAllAsync(userId)).ToList(),
                UserWorkTasks = (await _bll.UserWorkTasks.GetAllAsync(userId)).ToList()
            };
            return View(vm);
        }
        // GET: TimeTable
        public async Task<IActionResult> TimeWindowIndex()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var vm = new TimeTableIndexViewModel()
            {
                TimeWindows = (await _bll.TimeWindows.GetAllSortedAsync(userId)).ToList(),
                WorkTaskTimeWindows = (await _bll.WorkTaskTimeWindows.GetAllAsync(userId)).ToList()
            };
            return View(vm);
        }

        // GET: TimeTable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimeTableCreateEditTimeWindowViewModel vm)
        {
            
            if (ModelState.IsValid)
            {
                vm.TimeWindow.Id = Guid.NewGuid();
                vm.TimeWindow.UserId = Guid.Parse(_userManager.GetUserId(User));
                vm.TimeWindow.FreeTime = vm.TimeWindow.Until - vm.TimeWindow.From;
                _bll.TimeWindows.Add(vm.TimeWindow);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(TimeWindowIndex));
            }
            return View(vm);
        }

        // GET: TimeTable/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            if (id == null)
            {
                return NotFound();
            }

            var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(id.Value, userId);
            if (timeWindow == null)
            {
                return NotFound();
            }
            
            return View(new TimeTableCreateEditTimeWindowViewModel(){TimeWindow = timeWindow});
        }

        // POST: TimeTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TimeTableCreateEditTimeWindowViewModel vm)
        {
            if (id != vm.TimeWindow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vm.TimeWindow.UserId = Guid.Parse(_userManager.GetUserId(User));
                    vm.TimeWindow.FreeTime = vm.TimeWindow.Until - vm.TimeWindow.From;
                    _bll.TimeWindows.Update(vm.TimeWindow);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.TimeWindows.ExistsAsync(vm.TimeWindow.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TimeWindowIndex));
            }
            return View(vm);
        }

        // GET: TimeTable/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(id.Value, userId);
            if (timeWindow == null)
            {
                return NotFound();
            }

            return View(timeWindow);
        }

        // POST: TimeTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(id, userId);
            if (timeWindow != null)
            {
                await _bll.TimeWindows.RemoveAsync(timeWindow);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(TimeWindowIndex));
        }
        
        // GET: TimeTable/Add/5
        public async Task<IActionResult> Add(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var workTask = (await _bll.UserWorkTasks.FirstOrDefaultAsync(id.Value, userId))!.WorkTask;
            if (workTask == null)
            {
                return NotFound();
            }

            var vm = new TimeTableAddViewModel()
            {
                WorkTask = workTask,
                WorkTaskId = workTask.Id,
                TimeWindowsSelectList = new SelectList(await _bll.TimeWindows.GetAllAvailableAsync(id.Value, userId), nameof(TimeWindow.Id),
                    nameof(TimeWindow.From))
            };

            return View(vm);
        }

        // POST: TimeTable/Add/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TimeTableAddViewModel vm)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            if (ModelState.IsValid && vm.TimeWindowId != default)
            {
                var wttw = new WorkTaskTimeWindow()
                {
                    Id = Guid.NewGuid(),
                    TimeWindowId = vm.TimeWindowId,
                    WorkTaskId = vm.WorkTaskId
                };
                _bll.WorkTaskTimeWindows.Add(wttw);
                var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(vm.TimeWindowId, userId);
                var timeExpectancy = (await _bll.WorkTasks.FirstOrDefaultAsync(vm.WorkTaskId, userId))!.TimeExpectancy ?? TimeSpan.Zero;
                var timeSpent = (await _bll.UserWorkTasks.GetFirstOfWorkTaskAsync(vm.WorkTaskId, userId))!.TimeSpent ?? TimeSpan.Zero;
                var totalTime = TimeSpan.Zero < timeExpectancy - timeSpent ? timeExpectancy - timeSpent : TimeSpan.Zero;
                
                timeWindow!.FreeTime = timeWindow.FreeTime - totalTime;
                _bll.TimeWindows.Update(timeWindow);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.TimeWindowsSelectList = new SelectList(await _bll.TimeWindows.GetAllAvailableAsync(vm.WorkTaskId, userId),
                nameof(TimeWindow.Id),
                nameof(TimeWindow.From), vm.TimeWindowId);
            return View(vm);
        }
        
        // GET: TimeTable/Remove/5
        public async Task<IActionResult> Remove(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var workTask = await _bll.WorkTasks.FirstOrDefaultAsync(id.Value, userId);
            if (workTask == null)
            {
                return NotFound();
            }

            var wttw = await _bll.WorkTaskTimeWindows.GetFirstOfWorkTaskAsync(id.Value, userId);
            if (wttw == null)
            {
                return NotFound();
            }
            await _bll.WorkTaskTimeWindows.RemoveAsync(wttw.Id);
            
            var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(wttw.TimeWindowId, userId);
            var timeExpectancy = (await _bll.WorkTasks.FirstOrDefaultAsync(workTask.Id, userId))!.TimeExpectancy ?? TimeSpan.Zero;
            var timeSpent = (await _bll.UserWorkTasks.GetFirstOfWorkTaskAsync(workTask.Id, userId))!.TimeSpent ?? TimeSpan.Zero;
            var totalTime = TimeSpan.Zero < timeExpectancy - timeSpent ? timeExpectancy - timeSpent : TimeSpan.Zero;
                
            timeWindow!.FreeTime = timeWindow.FreeTime + totalTime;
            _bll.TimeWindows.Update(timeWindow);
            
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
         
        // POST: TimeTable/Add/5
        public async Task<IActionResult> Start(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var userWorkTask = await _bll.UserWorkTasks.FirstOrDefaultAsync(id.Value, userId);
            userWorkTask!.Status = EStatus.Pending;
            userWorkTask.WorkTask = null;
            _bll.UserWorkTasks.Update(userWorkTask);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        // GET: TimeTable/Add/5
        public async Task<IActionResult> Finish(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var workTask = (await _bll.UserWorkTasks.FirstOrDefaultAsync(id.Value, userId))!.WorkTask;
            if (workTask == null)
            {
                return NotFound();
            }

            var vm = new TimeTableFinishViewModel()
            {
                WorkTask = workTask,
                WorkTaskId = workTask.Id,
                UserWorkTask = await _bll.UserWorkTasks.FirstOrDefaultAsync(id.Value, userId)
            };

            return View(vm);
        }

        // POST: TimeTable/Add/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(TimeTableFinishViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(_userManager.GetUserId(User));
                vm.UserWorkTask.Status = EStatus.Completed;
                vm.UserWorkTask.WorkTask = null;
                _bll.UserWorkTasks.Update(vm.UserWorkTask);
                
                var timeWindow = await _bll.TimeWindows.FindByWorkTaskIdAsync(vm.UserWorkTask.WorkTaskId, userId);
                if (timeWindow != default)
                {
                    var timeExpectancy = (await _bll.WorkTasks.FirstOrDefaultAsync(vm.UserWorkTask.WorkTaskId, userId))!.TimeExpectancy ?? TimeSpan.Zero;
                    var timeSpent = (await _bll.UserWorkTasks.GetFirstOfWorkTaskAsync(vm.UserWorkTask.WorkTaskId, userId))!.TimeSpent ?? TimeSpan.Zero;
                    TimeSpan totalTime;
                    if (timeExpectancy < (vm.UserWorkTask.TimeSpent ?? TimeSpan.Zero))
                    {
                        if (timeExpectancy > timeSpent)
                        {
                            totalTime = timeExpectancy - timeSpent;
                        }
                        else
                        {
                            totalTime = TimeSpan.Zero;
                        }
                    }
                    else
                    {
                        totalTime = (vm.UserWorkTask.TimeSpent ?? TimeSpan.Zero) - timeSpent;
                    }
                    timeWindow!.FreeTime = timeWindow.FreeTime + totalTime;
                    _bll.TimeWindows.Update(timeWindow);
                }
                
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        
        
        // GET: TimeTable/Add/5
        public async Task<IActionResult> Pause(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var workTask = (await _bll.UserWorkTasks.FirstOrDefaultAsync(id.Value, userId))!.WorkTask;
            if (workTask == null)
            {
                return NotFound();
            }

            var vm = new TimeTableFinishViewModel()
            {
                WorkTask = workTask,
                WorkTaskId = workTask.Id,
                UserWorkTask = await _bll.UserWorkTasks.FirstOrDefaultAsync(id.Value, userId)
            };

            return View(vm);
        }

        // POST: TimeTable/Add/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pause(TimeTableFinishViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(_userManager.GetUserId(User));
                vm.UserWorkTask.Status = EStatus.Paused;
                vm.UserWorkTask.WorkTask = null;
                _bll.UserWorkTasks.Update(vm.UserWorkTask);
                
                var timeWindow = await _bll.TimeWindows.FindByWorkTaskIdAsync(vm.UserWorkTask.WorkTaskId, userId);
                if (timeWindow != default)
                {
                    var timeExpectancy =
                        (await _bll.WorkTasks.FirstOrDefaultAsync(vm.UserWorkTask.WorkTaskId, userId))!
                        .TimeExpectancy ?? TimeSpan.Zero;
                    var timeSpent =
                        (await _bll.UserWorkTasks.GetFirstOfWorkTaskAsync(vm.UserWorkTask.WorkTaskId, userId))!
                        .TimeSpent ?? TimeSpan.Zero;
                    TimeSpan totalTime;
                    if (timeExpectancy < (vm.UserWorkTask.TimeSpent ?? TimeSpan.Zero))
                    {
                        if (timeExpectancy > timeSpent)
                        {
                            totalTime = timeExpectancy - timeSpent;
                        }
                        else
                        {
                            totalTime = TimeSpan.Zero;
                        }
                    }
                    else
                    {
                        totalTime = (vm.UserWorkTask.TimeSpent ?? TimeSpan.Zero) - timeSpent;
                    }

                    timeWindow!.FreeTime = timeWindow.FreeTime + totalTime;
                    _bll.TimeWindows.Update(timeWindow);
                }

                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

    }
}
