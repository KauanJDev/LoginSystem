using LoginSystem.DataConfig;
using LoginSystem.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LoginSystem.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SectionRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void AddSection(UserModel user)
        {
            string value = JsonConvert.SerializeObject(user);
            _httpContextAccessor.HttpContext.Session.SetString("UserId", value);
        }
        public void DeleteSection()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserId");
        }

        public UserModel SearchSection()
        {
            string userSession = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userSession)) return null;

            return JsonConvert.DeserializeObject<UserModel>(userSession);
        }
    }
}
