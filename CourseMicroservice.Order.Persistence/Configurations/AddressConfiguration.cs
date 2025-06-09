using CourseMicroservice.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseMicroservice.Order.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).UseIdentityColumn();
            builder.Property(a => a.Province).HasMaxLength(50).IsRequired();
            builder.Property(a => a.District).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Line).HasMaxLength(200).IsRequired();
            builder.Property(a => a.ZipCode).HasMaxLength(20).IsRequired();
        }
    }
}
