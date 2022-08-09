using System.ComponentModel.DataAnnotations;

namespace UserandRoles.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
