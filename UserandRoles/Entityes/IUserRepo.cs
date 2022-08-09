using System.Security.Claims;
using UserandRoles.Models;
using UserandRoles.ViewModels;

namespace UserandRoles.Entityes
{
    public interface IUserRepo
    {
        Task CreateAsync(RegisterViewModel model);

        List<User> GetAll();

        Task<User> UserReturn(LoginViewModel model);

        Task<bool> VerifyPassword(string Password, byte[] passworHash, byte[] passwordSalt);

        IEnumerable<MapIndexViewModel> gets();

        IEnumerable<MapIndexViewModel> getss();


    }
}
