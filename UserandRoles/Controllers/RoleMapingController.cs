using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserandRoles.Entityes;
using UserandRoles.ViewModels;

namespace UserandRoles.Controllers
{
    public class RoleMapingController : Controller
    {
        private readonly IRoleMapRepo repo;

        public RoleMapingController(IRoleMapRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            var model = repo.GetAll();
            return View(model);
        }
        [HttpGet]
        public ViewResult Create() 
        {
            ViewData["role"] = new SelectList(repo.listrole(), "RoleId", "RoleName");
            ViewData["user"] = new SelectList(repo.listuser(), "Id", "UserName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleMapViewModel model) 
        {
            await repo.CreateAsync(model);

            return RedirectToAction("Index", "RoleMaping");
        }
    }
}
