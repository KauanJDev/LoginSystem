using LoginSystem.Models;

namespace LoginSystem.Repository
{
    public interface ILoginRepository
    {
        UserModel RegisterUser(string username,string email, string password);
        UserModel LoginUser(string email, string password);
    }
}
