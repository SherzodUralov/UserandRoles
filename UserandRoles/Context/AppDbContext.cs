using Microsoft.EntityFrameworkCore;
using UserandRoles.Models;

namespace UserandRoles.Context
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleMaping> RoleMapings { get; set; }

    }
}
