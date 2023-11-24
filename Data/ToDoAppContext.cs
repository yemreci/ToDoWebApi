using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Data
{
    public class ToDoAppContext : DbContext
    {
        public DbSet<ToDoEntity> ToDos { get; set; } = null;
        public DbSet<ListEntity> ToDoLists { get; set; } = null;

        public ToDoAppContext(DbContextOptions<ToDoAppContext> options) : base(options)
        {
            DbInitializer.Initialize(this);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoEntity>()
                .HasIndex(t => t.Id)
                .IsUnique();
        }
    }
}
