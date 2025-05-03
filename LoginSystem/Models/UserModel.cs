using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome de Usuário é obrigatório.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
