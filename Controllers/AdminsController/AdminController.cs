using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticEPAM.Models;

namespace PracticEPAM.Controllers.AdminsController
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataBaseSiteContext _context;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, DataBaseSiteContext context)
        {
            this.roleManager = roleManager;
            _context = context;
            _userManager = userManager;


        }
        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SetUserRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AllUserWithRoles()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SetUserRole(string name, string role)
        {
            IdentityUser user = await _userManager?.FindByNameAsync(name);
            IdentityRole _role = await roleManager?.FindByNameAsync(role);
            if (user != null && _role != null)
            {
                await _userManager.AddToRoleAsync(user, role);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Admin));

        }
        [HttpGet]
        public async Task<ActionResult> CreateRole()
        { return View(); }

        // GET: Admin/Create
        [HttpPost]
        public async Task<ActionResult> CreateRole(string roleName)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return View();
        }
    }
}
