using EasyGram.Data;
using EasyGram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using EasyGram.ViewModels;
using System;

namespace EasyGram.Controllers
{
    public class LessonsController : Controller
    {
        private readonly AppDbContext _context;

        public LessonsController(AppDbContext context)
        {
            _context = context;
        }

        // Список всех уроков
        public async Task<IActionResult> Index()
        {
            var userId = _context.Users
                .FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

            var solvedTaskIds = _context.TaskResults
                .Where(r => r.UserId == userId && r.IsCorrect)
                .Select(r => r.TaskItemId)
                .ToHashSet();

            var lessons = await _context.Lessons
                .Include(l => l.Tasks)
                .ToListAsync();

            var viewModel = lessons.Select(lesson =>
            {
                var total = lesson.Tasks.Count;
                var solved = lesson.Tasks.Count(t => solvedTaskIds.Contains(t.Id));

                return new LessonProgressViewModel
                {
                    LessonId = lesson.Id,
                    LessonTitle = lesson.Title,
                    TotalTasks = total,
                    SolvedTasks = solved
                };
            }).ToList();

            return View(viewModel);
        }


        // Страница конкретного урока с заданиями и материалами
        public async Task<IActionResult> Details(int id)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Tasks)
                .Include(l => l.Materials)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null)
                return NotFound();

            // Получение текущего пользователя
            var userId = _context.Users
                .FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

            // Получаем ID всех успешно решённых задач пользователем
            var solvedTaskIds = _context.TaskResults
                .Where(r => r.UserId == userId && r.IsCorrect)
                .Select(r => r.TaskItemId)
                .ToHashSet();

            var viewModel = new LessonTasksViewModel
            {
                LessonId = lesson.Id,
                LessonTitle = lesson.Title,
                LessonDescription = lesson.Description,
                Deadline = lesson.Deadline,
                Tasks = lesson.Tasks.Select(t => new TaskStatusViewModel
                {
                    TaskId = t.Id,
                    TaskTitle = t.Title,
                    TaskDescription = t.Description,
                    MaxScore = t.MaxScore,
                    IsCompleted = solvedTaskIds.Contains(t.Id)
                }).ToList(),
                Materials = lesson.Materials.Select(m => new MaterialViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Type = m.Type,
                    Url = m.Url
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
