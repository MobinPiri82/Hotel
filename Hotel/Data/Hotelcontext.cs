using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hotel.Data
{
    public class Hotelcontext : DbContext
    {
        public DbSet<Counrty> countries { get; set; }
        public DbSet<HotelInfo> hotels { get; set; }
        public DbSet<Personel> personels{ get; set; }

        public Hotelcontext(DbContextOptions<Hotelcontext> options)  : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.Entity<Hotel>()
            //    .HasOne(a => a.country)
            //    .WithMany(p => p.Hotels);
        }




        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options
        //        .UseLazyLoadingProxies()
        //        .UseSqlServer("");
        //}

    }
}
