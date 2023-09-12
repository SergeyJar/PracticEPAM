using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticEPAM.Models;
using System.Diagnostics;

namespace PracticEPAM.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataBaseSiteContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataBaseSiteContext context)
        {
            _logger = logger;
            _context = context;
        }
        public int ProductCount()
        {
            return _context.Products.Count();
        }
        public List<PracticEPAM.Models.Review> Reviews(int id)
        {
            var REVIEWS =_context.Reviews.Where(p=>p.IdProduct==id).ToList();
            return REVIEWS;
        }
        public async Task<IActionResult> ReviewsPage(int id)
        {
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product =_context.Products.Include(p => p.IdCategoriesNavigation).Where(p=>p.IdProduct==id).FirstOrDefault();
                if (product == null)
                {
                    return NotFound();
                }
                ViewModels.ReviewsPage reviews = new ViewModels.ReviewsPage();
                reviews.Reviews = Reviews(id);
                reviews.Product=product;
                return View(reviews);
           
        }

        public IActionResult MainPage(int page)
        {// ну типо по 9 на странице отображаем
            if (Math.Ceiling((double)ProductCount() / 9) >= page)
            {
                ViewBag.Count = ProductCount();
                    var product = (from prod in _context.Products
                        where (prod.Photo != null)
                        select new
                        {Photo=prod.Photo,
                            Name = prod.Name,
                            Description = prod.Description,
                            Id=prod.IdProduct
                        }).Skip((page) * 9).Take(9).AsEnumerable().ToList();
                    return View(product);
               
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewData["IdProduct"] = new SelectList(_context.Products.Where(P => P.IdProduct == id), "IdProduct", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdReview,IdUser,IdProduct,ReviewHtml, Name, Description")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MainPage));
            }
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", review.IdProduct);
            return View(review);
        }

        public IActionResult Privacy()
        {
            return View();
        }
      

        public async Task<IActionResult> ReviewsHtmlAsync(int id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdReview == id);
            if (review == null)
            {
                return NotFound();
            }

            var REVIEWS = _context.Reviews.Where(p => p.IdReview == id).FirstOrDefault();
            ViewBag.Html = REVIEWS.ReviewHtml;

            return View(review);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}