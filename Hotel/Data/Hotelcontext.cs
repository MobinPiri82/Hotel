using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hotel.Data
{
    public class Hotelcontext : DbContext
    {
        public DbSet<counrty> counrties { get; set; }
        public DbSet<HotelInfo> hotels { get; set; }
        public DbSet<Personel> personels{ get; set; }

        public Hotelcontext(DbContextOptions<Hotelcontext> options)  : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }




        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options
        //        .UseLazyLoadingProxies()
        //        .UseSqlServer("");
        //}

    }
}
