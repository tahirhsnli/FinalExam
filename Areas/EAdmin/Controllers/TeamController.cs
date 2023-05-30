using Business.DAL;
using Business.Models;
using Business.Utilities.Extensions;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Business.Areas.EAdmin.Controllers
{
    [Area("EAdmin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(int page = 1,int take = 3)
        {
            var team = await _context.Teams.Where(x => x.Isdeleted == false).Skip((page - 1) * take).Take(take).Include(x => x.Profession).ToListAsync();
            PaginateVM<Team> paginate = new PaginateVM<Team>()
            {
                Items = team,
                Currentpage = page,
                PageCount = PageCount(take)
            };
            return View(paginate);
        }
        private int PageCount(int take)
        {
            var count = _context.Teams.Where(x=>x.Isdeleted==false).Count();
            return (int)Math.Ceiling((double)count / take);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Team is null");
                return View();
            }
            if(team.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image is null");
                return View();
            }
            if (team.ImageFile.CheckSize(200))
            {
                ModelState.AddModelError("ImageFile", "Image is big");
                return View();
            }
            if (!team.ImageFile.CheckType("image"))
            {
                ModelState.AddModelError("ImageFile", "Image is wrong type");
                return View();
            }
            team.Image = await team.ImageFile.Savefile(_env.WebRootPath, "team");
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            return View(await _context.Teams.Where(x => x.Isdeleted == false).Include(x => x.Profession).FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Team team)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Team is null");
                return View();
            }
            Team? exist = await _context.Teams.Where(x => x.Isdeleted == false).Include(x => x.Profession).FirstOrDefaultAsync(x => x.Id == team.Id);
            if(exist == null)
            {
                ModelState.AddModelError("", "Team is null");
                return View();
            }
            if(team.ImageFile != null)
            {
                if (team.ImageFile.CheckSize(200))
                {
                    ModelState.AddModelError("ImageFile", "Image is big");
                    return View();
                }
                if (!team.ImageFile.CheckType("image"))
                {
                    ModelState.AddModelError("ImageFile", "Image is wrong type");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath, "assets", "img", "team", exist.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exist.Image = await team.ImageFile.Savefile(_env.WebRootPath, "team");
            }
            exist.Fullname = team.Fullname;
            exist.ProfessionId =team.ProfessionId;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Team? exist = await _context.Teams.Where(x => x.Isdeleted == false).Include(x => x.Profession).FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
            {
                ModelState.AddModelError("", "Team is null");
                return View();
            }
            string path = Path.Combine(_env.WebRootPath, "assets", "img", "team", exist.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            exist.Isdeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
