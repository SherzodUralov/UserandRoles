using UserandRoles.Models;
using UserandRoles.ViewModels;

namespace UserandRoles.Entityes
{
    public interface IRoleMapRepo
    {
        Task CreateAsync(RoleMapViewModel maping);
        Task UpdateAsync(RoleMaping maping);
        Task DeleteAsync(RoleMaping maping);
        List<MapIndexViewModel> GetAll();
        List<Role> listrole();
        List<User> listuser();
    }
}
