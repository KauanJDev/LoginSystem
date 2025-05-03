using LoginSystem.Models;

namespace LoginSystem.Repository
{
    public interface ISectionRepository 
    {
        void AddSection(UserModel user);
        UserModel SearchSection();
        void DeleteSection();

    }
}
