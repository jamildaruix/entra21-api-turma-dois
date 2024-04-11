using Microsoft.EntityFrameworkCore;

namespace minha_primeira_api.Models
{
    public class TodoContextDB : DbContext
    {
        public TodoContextDB(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItemModel> TodoItemModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItemModel>()
                        .Property(p => p.Cadastro)
                        .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
