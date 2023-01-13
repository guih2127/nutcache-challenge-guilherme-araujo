using EmployeesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(e => e.Id);
            builder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.Cpf).IsRequired().HasMaxLength(50);
            builder.Entity<Employee>().Property(p => p.StartDate).IsRequired();
            builder.Entity<Employee>().Property(p => p.BirthDate).IsRequired();
            builder.Entity<Employee>().Property(p => p.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Employee>().Property(p => p.Gender).IsRequired().HasMaxLength(100);
        }
    }
}
