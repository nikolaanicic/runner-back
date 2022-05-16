using Contracts.Models;
using Entities.ModelConfigurations;
using Entities.ModelConfigurations.UsersConfiguration;
using Entities.PasswordSecurity;
using Microsoft.EntityFrameworkCore;


namespace Entities.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ConsumerConfiguration());
            builder.ApplyConfiguration(new DelivererConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new AdminConfiguration(new PasswordManager()));
        }

        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Consumer> Consumer { get; set; }
        public DbSet<Deliverer> Deliverer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
