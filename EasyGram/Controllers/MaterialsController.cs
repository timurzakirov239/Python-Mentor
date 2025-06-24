using EasyGram.Data;
using EasyGram.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyGram.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly AppDbContext _context;

        public MaterialsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
                return NotFound();

            return View(material);
        }
    }
}
