using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JeBalance.SQLLite.Configuration
{
    internal class ResponseConfiguration : IEntityTypeConfiguration<ResponseSQLS>
    {
        public void Configure(EntityTypeBuilder<ResponseSQLS> builder)
        {
            builder.ToTable("Response")
                .HasKey(response => response.Id);

            builder.Property(response => response.Timestamp)
                .IsRequired();

            builder.Property(response => response.ResponseType)
                .IsRequired();

        }
    }
}