namespace WebAPI.Extensions
{
    public static class WebUIServicesExtensions
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
          

            services.AddControllersWithViews();
            services.AddRazorPages();

            return services;
        }
    }
}
