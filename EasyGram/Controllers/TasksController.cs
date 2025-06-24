using EasyGram.Data;
using EasyGram.ViewModels;
using Microsoft.AspNetCore.Mvc;
using EasyGram.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

public class TasksController : Controller
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Details(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null) return NotFound();

        var vm = new TaskDetailsViewModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            MaxScore = task.MaxScore,
            UserCode = "",
            JudgeResult = "",
            SubmissionTime = null
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Details(int id, string code)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null) return NotFound();

        var tests = _context.TaskTests
            .Where(t => t.TaskItemId == task.Id)
            .ToList();

        bool allPassed = true;
        StringBuilder resultBuilder = new();
        var client = new HttpClient();

        foreach (var test in tests)
        {
            var payload = new
            {
                language = "python",
                version = "3.10.0",
                stdin = test.Input,
                files = new[]
                {
                    new { name = "main.py", content = code }
                }
            };

            var json = JsonConvert.SerializeObject(payload);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://emkc.org/api/v2/piston/execute", contentJson);
                var resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PistonResponse>(resultJson);

                string output = result.run.stdout?.Trim() ?? "";
                string error = result.run.stderr?.Trim() ?? "";
                int returnCode = result.run.code;
                string expected = test.ExpectedOutput?.Trim() ?? "";

                string formattedInput = test.Input.Replace("\\n", "\n");

                if (returnCode != 0 || !string.IsNullOrWhiteSpace(error))
                {
                    resultBuilder.AppendLine($"Ввод:\n{formattedInput}\nОшибка выполнения (код {returnCode}): {error}");
                    allPassed = false;
                    break;
                }
                else if (string.IsNullOrWhiteSpace(output))
                {
                    resultBuilder.AppendLine($"Ввод:\n{formattedInput}\nВывод отсутствует.");
                    allPassed = false;
                    break;
                }
                else if (output != expected)
                {
                    resultBuilder.AppendLine($"Ввод:\n{formattedInput}\nожидалось: \"{expected}\", получено: \"{output}\"");
                    allPassed = false;
                    break;
                }
            }
            catch (Exception ex)
            {
                resultBuilder.AppendLine($"Ввод:\n{test.Input}\nОшибка выполнения запроса: {ex.Message}");
                allPassed = false;
                break;
            }
        }

        // ✅ Если все тесты пройдены — записать результат в TaskResults
        if (allPassed)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            if (userId != null)
            {
                var exists = _context.TaskResults.Any(e => e.UserId == userId && e.TaskItemId == task.Id);
                if (!exists)
                {
                    _context.TaskResults.Add(new TaskResult
                    {
                        UserId = userId,
                        TaskItemId = task.Id,
                        Score = task.MaxScore,
                        IsCorrect = true,
                        SubmittedAt = DateTime.UtcNow
                    });
                    await _context.SaveChangesAsync();
                }
            }
        }

        string judgeResult = allPassed
            ? "✅ Все тесты пройдены!"
            : resultBuilder.ToString();

        var vm = new TaskDetailsViewModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            MaxScore = task.MaxScore,
            UserCode = code,
            JudgeResult = judgeResult,
            SubmissionTime = DateTime.Now
        };

        return View(vm);
    }
}
