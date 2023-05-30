using Business.DAL;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Business.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM()
            {
                Teams = await _context.Teams.Where(x => x.Isdeleted == false).Include(x => x.Profession).ToListAsync()
            };
            return View(vm);
        }
    }
}
