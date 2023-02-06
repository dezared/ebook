using eBook_backend.Models.Entites;
using eBook_backend.Models.Entities.Identify;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eBook_backend
{
    /// <summary>
    /// Контекст для работы с базой данных и EF CORE.
    /// </summary>
    public class ApplicationContext : DbContext, IDesignTimeDbContextFactory<ApplicationContext>
    {
        /// <inheritdoc/>
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString") ?? "ebook-db;Database=ebook-database;Username=postgres;Password=admin;Port=5432");

            return new ApplicationContext(optionsBuilder.Options);
        }

        /// <inheritdoc />
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        /// <inheritdoc />
        public ApplicationContext()
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
