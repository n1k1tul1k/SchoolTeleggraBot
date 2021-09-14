using Microsoft.EntityFrameworkCore;
using SchoolTelegramBot.AppCore.Models;

namespace SchoolTelegramBot.AppCore.DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<LessonModel> Lessons { get; set; }
        public DbSet<StateModel> States { get; set; }
        public ApplicationContext()
        {
             Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("x");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}