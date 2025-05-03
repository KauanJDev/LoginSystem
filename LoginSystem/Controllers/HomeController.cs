using Microsoft.AspNetCore.Mvc;
using LoginSystem.Repository;

namespace LoginSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public HomeController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public IActionResult Index()
        {
            var user = _sectionRepository.SearchSection();

            if (user == null)
            {
                TempData["ErrorMessage"] = "Você precisa estar logado para acessar essa página.";
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Username = user.Username;
            return View();
        }
    }
}
