using FunnyMaps.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FunnyMaps.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
               
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Joke> Jokes { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
