using System.Security.Cryptography;
using System.Text;
using LoginSystem.DataConfig;
using LoginSystem.Models;

namespace LoginSystem.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext _context;
        public LoginRepository(DataContext context)
        {
            _context = context;
        }

        public UserModel LoginUser(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("All fields are required!");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == HashPassword(password));

            if (user == null)
            {
                throw new InvalidOperationException("Invalid email or password!");
            }

            return user;
        }

        public UserModel RegisterUser(string username, string email, string password)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == email))
                {
                    throw new InvalidOperationException("This Email already used!");
                }
                else if (_context.Users.Any(u => u.Username == username))
                {
                    throw new InvalidOperationException("This Username already used!");
                }
                else if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    throw new InvalidOperationException("All fields are required!");
                }
                    var user = new UserModel
                    {
                        Email = email,
                        Password = HashPassword(password),
                        Username = username,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                _context.Users.Add(user);
                _context.SaveChanges();

                return user;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error to register user: " + ex.Message);
            }
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }


    }
}
