using FunnyMaps.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FunnyMaps.Server.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
               
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Joke> Jokes { get; set; }
        public DbSet<Location> Locations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Joke>()
               .HasOne(u => u.User)
               .WithMany(j => j.Jokes)
               .HasForeignKey(j => j.UserId);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() 
                {
                    Id = "1",
                    Name = ApplicationRole.Admin,
                    NormalizedName = ApplicationRole.Admin.ToUpper(),
                },
                new IdentityRole()
                {
                    Id = "2",
                    Name = ApplicationRole.User,
                    NormalizedName = ApplicationRole.User.ToUpper(),
                }
                );           
        }
    }

}
