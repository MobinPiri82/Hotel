using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Data.EntityConfigurations
{
    public class HotelEntityConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder
                .HasOne(a => a.country)
                .WithMany(p => p.Hotels)
                .HasForeignKey(h => h.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .Property(h => h.CountryId)
                .IsRequired()
                .HasMaxLength(28);
        }
    }
}
