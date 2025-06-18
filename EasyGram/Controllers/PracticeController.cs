using EasyGram.Data;
using EasyGram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace EasyGram.Controllers
{
    public class LessonsController : Controller
    {
        private readonly AppDbContext _context;

        public LessonsController(AppDbContext context)
        {
            _context = context;
        }

        // Список уроков
        public async Task<IActionResult> Index()
        {
            var lessons = await _context.Lessons
                .Include(l => l.Tasks)
                .ToListAsync();

            return View(lessons);
        }

        // Страница одного урока (с задачами и материалами)
        public async Task<IActionResult> Details(int id)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Tasks)
                .Include(l => l.Materials)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null)
                return NotFound();

            return View(lesson);
        }
    }
}