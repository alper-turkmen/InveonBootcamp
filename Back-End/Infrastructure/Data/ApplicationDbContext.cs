using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Video> Videos { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

   builder.Entity<Course>(entity =>
    {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Title).IsRequired().HasMaxLength(200);

        entity.HasOne(c => c.Teacher)
              .WithMany() 
              .HasForeignKey(c => c.TeacherId);
    });

            builder.Entity<Video>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Url).IsRequired();
            });

                   builder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

                builder.Entity<Order>()
                    .HasOne(o => o.Course)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CourseId);
                }
    }
}