using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Models;
using Microsoft.AspNetCore.Identity;

namespace TaskFlow.API.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define your DbSets here, for example:
        public DbSet<TaskItem> Tasks { get; set; }
    }
}