using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Hotel.Data.EntityConfigurations
{
    public class PersonelEntityConfiguration : IEntityTypeConfiguration<Personel>
    {
        public void Configure(EntityTypeBuilder<Personel> builder)
        {
            builder
                .HasOne(b => b.WorkingHotel)
                .WithMany(c => c.personels)
                .HasForeignKey(i => i.HotelId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.
                Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasKey(a => a.id);

            builder.Property(a => a.id).
                ValueGeneratedOnAdd().
                UseIdentityColumn();
        }

    }
}
