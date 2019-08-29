using Microsoft.EntityFrameworkCore;
using Sovhoz22.Models.PhoneModels;
using Sovhoz22.Models.OrderModels;
using Sovhoz22.Models.CommentModels;
using Sovhoz22.Models.FileModels;
namespace Sovhoz22.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public MobileContext(DbContextOptions<MobileContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}