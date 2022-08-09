using UserandRoles.Context;
using UserandRoles.Entityes;
using UserandRoles.Models;
using UserandRoles.ViewModels;

namespace UserandRoles.Repository
{
    public class RoleMapRepo : IRoleMapRepo
    {
        private readonly AppDbContext context;

        public RoleMapRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(RoleMapViewModel maping)
        {
            RoleMaping newrolemap = new RoleMaping
            {
                RoleId = maping.RoleId,
                UserId = maping.UserId,
            };
            await context.RoleMapings.AddAsync(newrolemap);
            await context.SaveChangesAsync();
        }

        public Task DeleteAsync(RoleMaping maping)
        {
            throw new NotImplementedException();
        }

        public List<MapIndexViewModel> GetAll()
        {
            var model = (from u in context.Users
                         join m in context.RoleMapings
                         on u.Id equals m.UserId
                         join r in context.Roles
                         on m.RoleId equals r.RoleId
                         select new MapIndexViewModel
                         {
                             FirstName = u.FirstName,
                             UserName = u.UserName,
                             RoleName = r.RoleName
                         });
            return model.ToList();
        }

        public List<Role> listrole()
        {
            return context.Roles.ToList();
        }

        public List<User> listuser()
        {
            return context.Users.ToList();
        }

        public Task UpdateAsync(RoleMaping maping)
        {
            throw new NotImplementedException();
        }
    }
}
