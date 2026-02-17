using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAppNotes.DataAccess.Data;
using WebAppNotes.DataAccess.Interfaces;
using WebAppNotes.DataAccess.Repositories;

namespace WebAppNotes.DataAccess.DI
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            services.AddDbContext<AppDbContext>(optionsBuilder);
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            return services;
        }
    }
}
