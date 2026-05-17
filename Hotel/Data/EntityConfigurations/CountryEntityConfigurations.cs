using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Hotel.Data.EntityConfigurations;

public class CountryEntityConfigurations : IEntityTypeConfiguration<Counrty>
{
    public void Configure(EntityTypeBuilder<Counrty> builder)
    {
        builder.
            HasMany(a => a.personels).
            WithOne(h => h.Nationality).
            HasForeignKey(d => d.CountryId).
            OnDelete(DeleteBehavior.Restrict);

        builder.HasKey(a => a.CountryId);

        builder.Property(a => a.CountryId).
            ValueGeneratedOnAdd().
            UseIdentityColumn();
        
    }
}
