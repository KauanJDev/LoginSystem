using LoginSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoginSystem.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //limit user for enter in home page.
            string session = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(session)) return Content(string.Empty);
            var user = JsonConvert.DeserializeObject<UserModel>(session);
            return View(user);
        }
    }
}

