using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserandRoles.Context;
using UserandRoles.Entityes;
using UserandRoles.Models;
using UserandRoles.ViewModels;

namespace UserandRoles.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext context;

        public UserRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(RegisterViewModel model)
        {
            byte[] passwordHash, passwordSalt;

            CreatePassworHash(model.Password, out passwordHash, out passwordSalt);

            User newuser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserName = model.Email
            };
            await context.Users.AddAsync(newuser);
            await context.SaveChangesAsync();

        }

        public void CreatePassworHash(string Password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) 
            {
                passwordSalt = hmac.Key;

                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public IEnumerable<MapIndexViewModel> gets()
        {
            var model = (from u in context.Users
                         join m in context.RoleMapings
                         on u.Id equals m.UserId
                         join r in context.Roles
                         on m.RoleId equals r.RoleId
                         where r.RoleName == "Superadmin"
                         select new MapIndexViewModel
                         {
                             UserName = u.UserName
                         });
            return model.ToArray();
        }

        public IEnumerable<MapIndexViewModel> getss()
        {
            var model = (from u in context.Users
                         join m in context.RoleMapings
                         on u.Id equals m.UserId
                         join r in context.Roles
                         on m.RoleId equals r.RoleId
                         where r.RoleName == "admin"
                         select new MapIndexViewModel
                         {
                             UserName = u.UserName
                         });
            return model.ToArray();
        }

        public async Task<User> UserReturn(LoginViewModel model)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.UserName == model.Email);

        }

        public async Task<bool> VerifyPassword(string Password, byte[] passworHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
            {
                var computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                for (int i = 0; i < computedhash.Length; i++) 
                {
                    if (computedhash[i] != passworHash[i])
                        return false;
                }

            }
            return true;
        }
    }
}
