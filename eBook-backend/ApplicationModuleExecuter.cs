using eBook_backend;
using eBook_backend.Models.Entites;
using eBook_backend.Models.Entities.Identify;
using eBook_backend.Repositories;
using eBook_backend.Services;
using eBook_backend.Services.Auth;
using Microsoft.EntityFrameworkCore;

namespace surfis_backend
{
    /// <summary>
    /// Класс для инциализации сервисных модулей
    /// </summary>
    public static class ApplicationModuleExecuter
    {
        /// <summary>
        /// Подключаем к DI все нужные нам сервисы и репозитории.
        /// </summary>
        public static void RequiredProvideServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddTransient<IDataBaseSeederService, DataBaseSeederService>();
            serviceDescriptors.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            serviceDescriptors.AddTransient<IAuthenticationService, AuthenticationService>();

            serviceDescriptors.AddTransient<IEntityBaseRepository<Book>, EntityBaseRepository<Book>>();
            serviceDescriptors.AddTransient<IEntityBaseRepository<Author>, EntityBaseRepository<Author>>();
            serviceDescriptors.AddTransient<IEntityBaseRepository<User>, EntityBaseRepository<User>>();

            serviceDescriptors.AddTransient<IBookService, BookService>();
            serviceDescriptors.AddTransient<IAuthorService, AuthorService>();
            serviceDescriptors.AddTransient<IUserService, UserService>();
        }

        /// <summary>
        /// Мигририуем базу данныех ( обновляем контекст базы данных на последнюю мигарцию ).
        /// </summary>
        public static void MigrateDatabase(this IServiceCollection serviceDescriptors)
        {
            var serviceProvider = serviceDescriptors.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<ApplicationContext>();
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
        }

        /// <summary>ф
        /// Добавляем первоначальные данные для базы данных.
        /// </summary>
        public static async Task SeedDatabase(this IServiceCollection serviceDescriptors)
        {
            var serviceProvider = serviceDescriptors.BuildServiceProvider();
            var dbSeeder = serviceProvider.GetRequiredService<IDataBaseSeederService>();
            await dbSeeder.SeedDatabaseUserIdentify();
        }
    }
}
