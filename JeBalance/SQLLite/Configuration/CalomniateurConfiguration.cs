using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JeBalance.SQLLite.Configuration
{
    internal class CalomniateurConfiguration : IEntityTypeConfiguration<CalomniateurSQLS>
    {
        public void Configure(EntityTypeBuilder<CalomniateurSQLS> builder)
        {
            builder.ToTable("CALOMNIATEUR")
                .HasKey(calumniator => calumniator.Id);
        }
    }
}