using Business.DAL;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Business.Areas.EAdmin.Controllers
{
    [Area("EAdmin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Settings.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Setting setting)
        {
            Setting? exist = await _context.Settings.FirstOrDefaultAsync(x => x.Id == setting.Id);
            if(exist == null)
            {
                ModelState.AddModelError("", "Value is null");
                return View();
            }
            exist.Value = setting.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
