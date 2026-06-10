using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Models;

namespace TaskFlow.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define your DbSets here, for example:
        public DbSet<TaskItem> Tasks { get; set; }
    }
}