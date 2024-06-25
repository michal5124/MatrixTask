using IO.Swagger.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace IO.Swagger.Repository
{
    
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

    }

}
