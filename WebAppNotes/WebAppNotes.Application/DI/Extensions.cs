using Microsoft.Extensions.DependencyInjection;
using WebAppNotes.Application.Interfaces;
using WebAppNotes.Application.Services;

namespace WebAppNotes.Application.DI
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ITagService, TagService>();

            return services;
        }
    }
}
