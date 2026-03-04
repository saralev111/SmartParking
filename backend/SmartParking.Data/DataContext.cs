using Microsoft.EntityFrameworkCore;
using SmartParking.Core.Entities;

namespace SmartParking
{
    public class DataContext: DbContext
    {
        public DbSet<Car> cars { get; set; }
        public DbSet<Parking> parkings { get; set; }
        public DbSet<Spot> spots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=smartParking_db");
            //optionsBuilder.LogTo(m => Console.WriteLine(m));
        }

    
    }
}

                                                           