using Microsoft.EntityFrameworkCore;
using StoreFlow.Entities;
using System.Security.Principal;

namespace StoreFlow.Context
{
    public class StoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-8DBIL9G;initial Catalog=StoreFlowDb;integrated Security=true;trust server certificate=true");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> orders { get; set; }    
        public DbSet<Activity> Activities { get; set; }    
        public DbSet<Todo> Todos { get; set; }    
        public DbSet<Message> Messages { get; set; }    
    }
}
