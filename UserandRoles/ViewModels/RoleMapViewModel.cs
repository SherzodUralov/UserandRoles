using System.ComponentModel.DataAnnotations;

namespace UserandRoles.ViewModels
{
    public class RoleMapViewModel
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
