using System.Reflection.Emit;
using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JeBalance.SQLLite.Configuration
{
    internal class DenonciationConfiguration : IEntityTypeConfiguration<DenonciationSQLS>
    {
        public void Configure(EntityTypeBuilder<DenonciationSQLS> builder)
        {

            builder.Property(denonciation => denonciation.Timestamp)
                .IsRequired();

            builder.Property(denonciation => denonciation.Offense)
                .IsRequired();

            builder.Property(denonciation => denonciation.EvasionCountry)
                .HasMaxLength(100);

            builder.HasIndex(d => d.DenonciationId)
                 .IsUnique();
        }
    }
}