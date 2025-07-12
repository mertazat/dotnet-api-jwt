using ApiJWT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ApiJWT;

public class CrmDbContext : DbContext
{
    public DbSet<User> Users
    {
        get; set;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("UsersInMemory");
    }
}
