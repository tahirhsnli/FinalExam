using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels
{
    public class RegisterVM
    {
        public string Fullname { get; set; } = null!;
        public string Username { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
