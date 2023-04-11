using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace PMS.Infrastructure.Data.Context
{
    public class PmsDbContext : DbContext
    {
        public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}