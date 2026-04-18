using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Data.EntityConfigurations
{
    public class HotelEntityConfiguration : IEntityTypeConfiguration<HotelInfo>
    {
        public void Configure(EntityTypeBuilder<HotelInfo> builder)
        {
            builder
                .HasOne(a => a.Country)
                .WithMany(p => p.Hotels)
                .HasForeignKey(h => h.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
