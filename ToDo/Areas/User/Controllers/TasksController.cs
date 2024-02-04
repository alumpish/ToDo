using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.DataAccess.Repository;
using ToDo.Models;

namespace ToDo.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class TasksController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public TasksController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            IEnumerable<ToDoTask> tasks = _unitOfWork.ToDoTask.GetAll(u => u.UserId == userId && !u.IsDeleted);
            return View(tasks);
        }

        public IActionResult Archived()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            IEnumerable<ToDoTask> tasks = _unitOfWork.ToDoTask.GetAll(u => u.UserId == userId && u.IsDeleted);
            return View(tasks);
        }

        public IActionResult Upsert(int? id)
        {
            ToDoTask task = new ToDoTask();
            task.Date = DateOnly.FromDateTime(DateTime.Now);

            if (id == null)
            {
                return View(task);
            }

            task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ToDoTask task)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            task.UserId = userId;

            task.IsDeleted = false;
            task.IsCompleted = false;

            if (task.Id == 0)
            {
                _unitOfWork.ToDoTask.Add(task);
            }
            else
            {
                _unitOfWork.ToDoTask.Update(task);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            ToDoTask task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            return View(task);
        }

        public IActionResult Archive(int id)
        {
            ToDoTask task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            task.IsDeleted = true;
            _unitOfWork.ToDoTask.Update(task);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UnArchive(int id)
        {
            ToDoTask task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            task.IsDeleted = false;
            _unitOfWork.ToDoTask.Update(task);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Archived));
        }

        public IActionResult MarkAsCompleted(int id)
        {
            ToDoTask task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            task.IsCompleted = true;
            _unitOfWork.ToDoTask.Update(task);
            _unitOfWork.Save();

            var referringUrl = Request.Headers["Referer"].ToString();
            if (referringUrl.Contains("Archived"))
            {
                return RedirectToAction(nameof(Archived));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MarkAsNotCompleted(int id)
        {
            ToDoTask task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            task.IsCompleted = false;
            _unitOfWork.ToDoTask.Update(task);
            _unitOfWork.Save();

            var referringUrl = Request.Headers["Referer"].ToString();
            if (referringUrl.Contains("Archived"))
            {
                return RedirectToAction(nameof(Archived));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MarkAsImportant(int id)
        {
            ToDoTask task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            task.IsImportant = true;
            _unitOfWork.ToDoTask.Update(task);
            _unitOfWork.Save();

            var referringUrl = Request.Headers["Referer"].ToString();
            if (referringUrl.Contains("Archived"))
            {
                return RedirectToAction(nameof(Archived));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MarkAsNotImportant(int id)
        {
            ToDoTask task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            task.IsImportant = false;
            _unitOfWork.ToDoTask.Update(task);
            _unitOfWork.Save();

            var referringUrl = Request.Headers["Referer"].ToString();
            if (referringUrl.Contains("Archived"))
            {
                return RedirectToAction(nameof(Archived));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            ToDoTask task = _unitOfWork.ToDoTask.Get(u => u.Id == id);
            _unitOfWork.ToDoTask.Remove(task);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
