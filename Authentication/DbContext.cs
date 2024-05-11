using Authentication.Domain;
using Microsoft.EntityFrameworkCore;

namespace Authentication
{
    public class DbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options): base(options)
        {
            
        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
