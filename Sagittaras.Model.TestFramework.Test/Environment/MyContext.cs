using Microsoft.EntityFrameworkCore;

namespace Sagittaras.Model.TestFramework.Test.Environment
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            });
        }
    }
}