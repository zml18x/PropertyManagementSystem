using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PMS.Infrastructure.Data.Context;

namespace PMS.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopeProvider = scope.ServiceProvider;
                var context = scopeProvider.GetRequiredService<PmsDbContext>();
                context.Database.Migrate();
            }
        }
    }
}