using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Hotel.Data.EntityConfigurations
{
    public class CountryEntityConfigurations : IEntityTypeConfiguration<counrty>
    {
        public void Configure(EntityTypeBuilder<counrty> builder)
        {
            builder
                .HasMany(a => a.personels)
                .WithOne(h => h.Nationality)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
