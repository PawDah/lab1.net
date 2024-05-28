using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using lab1.net.Models;

namespace lab1.net.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<lab1.net.Models.ShoppingItem> ShoppingItem { get; set; } = default!;
        public DbSet<lab1.net.Models.ShoppingList> ShoppingList { get; set; } = default!;
    }
}
