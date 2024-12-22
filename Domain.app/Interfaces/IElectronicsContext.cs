using Domain.app.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.app.Interfaces
{
    public interface IElectronicsContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync();
    }
}
