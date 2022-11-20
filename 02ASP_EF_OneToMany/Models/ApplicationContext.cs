using Microsoft.EntityFrameworkCore;
using _02ASP_EF_OneToMany.Models;

namespace _02ASP_EF_OneToMany.Models
{
    // контекст отображения на базу данных
    public class ApplicationContext: DbContext
    {
        public DbSet<Edition> Editions { get; set; } = null!;
        public DbSet<Category> Categorys { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // создание БД если ее нет
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Category>()
                .HasData(
                    new Category { Id = 1, CategoryName = "Журнал" },
                    new Category { Id = 2, CategoryName = "Журнал" },
                    new Category { Id = 3, CategoryName = "Журнал" }
            );
        }
    }
}
