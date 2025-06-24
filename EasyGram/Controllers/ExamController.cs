using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyGram.Data;
using EasyGram.Models;
using System.Security.Claims;
using EasyGram.ViewModels.Exam;
using System.Text;

namespace EasyGram.Controllers
{
    public class ExamController : Controller
    {
        private readonly AppDbContext _context;

        public ExamController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var exams = await _context.Exams
                .OrderBy(e => e.Id)
                .ToListAsync();

            return View(exams);
        }

        [HttpGet]
        public async Task<IActionResult> Start(int id)
        {
            var exam = await _context.Exams
                .Include(e => e.ExamTasks)
                .ThenInclude(t => t.Tests)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exam == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Получаем список id задач, которые пользователь решил
            var solvedTaskIds = await _context.ExamResults
                .Where(r => r.UserId == userId && r.ExamId == id && r.IsPassed)
                .SelectMany(r => r.Exam.ExamTasks.Select(t => t.Id))
                .ToListAsync();

            var viewModel = new ExamViewModel
            {
                ExamTitle = exam.Title,
                ExamDescription = exam.Description,
                Duration = exam.Duration,
                Tasks = exam.ExamTasks.Select(t => new ExamTaskViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    MaxScore = t.MaxScore,
                    IsCompleted = solvedTaskIds.Contains(t.Id)
                }).ToList()
            };

            return View(viewModel);
        }


        // Экшн для просмотра экзаменационного задания
        [HttpGet]
        public async Task<IActionResult> TaskDetails(int id)
        {
            var task = await _context.ExamTasks.FindAsync(id);
            if (task == null) return NotFound();

            var viewModel = new ExamTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                MaxScore = task.MaxScore
            };
            return View(viewModel);
        }

        // Обработчик для отправки решения
        [HttpPost]
        public async Task<IActionResult> CheckTask(int TaskId, string UserCode)
        {
            var task = await _context.ExamTasks
                .Include(t => t.Tests)
                .FirstOrDefaultAsync(t => t.Id == TaskId);
            if (task == null) return NotFound();

            bool allPassed = true;
            StringBuilder checkLog = new();

            foreach (var test in task.Tests)
            {
                var payload = new
                {
                    language = "python",
                    version = "3.10.0",
                    stdin = test.Input,
                    files = new[] { new { name = "main.py", content = UserCode } }
                };

                using var client = new HttpClient();
                var content = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(payload),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync("https://emkc.org/api/v2/piston/execute", content);
                var json = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PistonResponse>(json);

                var output = result.run.stdout?.Trim() ?? "";
                var error = result.run.stderr?.Trim() ?? "";
                var returnCode = result.run.code;
                var expected = test.ExpectedOutput?.Trim() ?? "";

                string formattedInput = test.Input.Replace("\\n", "\n");

                if (returnCode != 0 || !string.IsNullOrWhiteSpace(error))
                {
                    checkLog.AppendLine($"Ввод:\n{formattedInput}\nОшибка выполнения (код {returnCode}): {error}");
                    allPassed = false;
                    break;
                }
                else if (string.IsNullOrWhiteSpace(output))
                {
                    checkLog.AppendLine($"Ввод:\n{formattedInput}\nВывод отсутствует.");
                    allPassed = false;
                    break;
                }
                else if (output != expected)
                {
                    checkLog.AppendLine($"Ввод:\n{formattedInput}\nОжидалось: \"{expected}\", получено: \"{output}\"");
                    allPassed = false;
                    break;
                }
            }

            string resultText = allPassed
                ? "✅ Все тесты пройдены!"
                : checkLog.ToString();

            ViewBag.Result = resultText;

            var viewModel = new ExamTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                MaxScore = task.MaxScore,
                UserCode = UserCode,
                Tests = task.Tests.Select(t => new ExamTaskTestViewModel
                {
                    Input = t.Input,
                    ExpectedOutput = t.ExpectedOutput
                }).ToList()
            };
            return View("TaskDetails", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Submit(Dictionary<int, string> answers)
        {
            var exam = await _context.Exams
                .Include(e => e.ExamTasks)
                .ThenInclude(t => t.Tests)
                .FirstOrDefaultAsync();

            if (exam == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            int correct = 0;

            foreach (var task in exam.ExamTasks)
            {
                if (answers.TryGetValue(task.Id, out string userCode))
                {
                    bool passedAll = true;

                    foreach (var test in task.Tests)
                    {
                        var payload = new
                        {
                            language = "python",
                            version = "3.10.0",
                            stdin = test.Input,
                            files = new[] { new { name = "main.py", content = userCode } }
                        };

                        using var client = new HttpClient();
                        var content = new StringContent(
                            Newtonsoft.Json.JsonConvert.SerializeObject(payload),
                            System.Text.Encoding.UTF8,
                            "application/json");

                        var response = await client.PostAsync("https://emkc.org/api/v2/piston/execute", content);
                        var json = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PistonResponse>(json);

                        var output = result.run.stdout?.Trim() ?? "";
                        var expected = test.ExpectedOutput?.Trim() ?? "";

                        if (output != expected)
                        {
                            passedAll = false;
                            break;
                        }
                    }

                    if (passedAll)
                        correct++;
                }
            }

            var resultEntry = new ExamResult
            {
                ExamId = exam.Id,
                UserId = userId,
                SubmittedAt = DateTime.UtcNow,
                Score = correct,
                IsPassed = correct >= exam.ExamTasks.Count * 0.6
            };

            _context.ExamResults.Add(resultEntry);
            await _context.SaveChangesAsync();

            return RedirectToAction("ExamResult");
        }

        public async Task<IActionResult> ExamResult()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _context.ExamResults
                .Include(r => r.Exam)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.SubmittedAt)
                .FirstOrDefaultAsync();

            if (result == null)
                return NotFound();

            return View(result);
        }
    }
}
