using Domain.app.Entities;
using Domain.app.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.app.DBConfigurations
{
    public class ElectronicsContext : IdentityDbContext<User,
                                                   Role,
                                                   int,
                                                   IdentityUserClaim<int>,
                                                   IdentityUserRole<int>,
                                                   IdentityUserLogin<int>,
                                                   IdentityRoleClaim<int>,
                                                   IdentityUserToken<int>>, IElectronicsContext
    {
        public ElectronicsContext(DbContextOptions<ElectronicsContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ElectronicsContext).Assembly);
        }

        DbSet<Category> IElectronicsContext.Categories { get; set; }
        DbSet<Product> IElectronicsContext.Products { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
