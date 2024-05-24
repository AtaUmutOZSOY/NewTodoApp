using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddApplicationMediatR(this IServiceCollection services)
        {
            var assemblies = new Assembly[]
            {
                typeof(Application.TodoLists.Commands.Create.CreateTodoListCommandHandler).Assembly,
                typeof(Application.TodoLists.Commands.Update.UpdateTodoListCommandHandler).Assembly,
                typeof(Application.TodoLists.Queries.GetAllTodoLists.GetAllTodoListsQueryHandler).Assembly
            };

            services.AddMediatR(assemblies);

            return services;
        }
    }
}
