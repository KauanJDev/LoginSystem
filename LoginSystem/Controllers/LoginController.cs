using LoginSystem.Repository;
using LoginSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LoginSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ISectionRepository _sectionRepository;

        public LoginController(ISectionRepository section,ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            _sectionRepository = section;
        }

        public IActionResult Index()
        {
            if(_sectionRepository.SearchSection() != null)
            {
                TempData["ErrorMessage"] = "Você já está logado!";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            _sectionRepository.DeleteSection();
            TempData["SuccessMessage"] = "Logout realizado com sucesso!";
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            try
            {
                var user = _loginRepository.RegisterUser(model.Username, model.Email, model.Password);
                TempData["SuccessMessage"] = "Usuário registrado com sucesso! Bem-vindo, " + user.Username;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao tentar registrar o usuário: " + ex.Message);
                return View("Register", model);
            }
        }
        [HttpPost]
        public IActionResult Login(UserModel model)
        {

                var user = _loginRepository.LoginUser(model.Email, model.Password);

                _sectionRepository.AddSection(user);

                TempData["SuccessMessage"] = $"Login realizado com sucesso! Bem-vindo, {user.Username}";

                return RedirectToAction("Index", "Home");

        }
    }
}

