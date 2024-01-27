using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JeBalance.SQLLite.Configuration
{
    internal class PersonConfiguration : IEntityTypeConfiguration<PersonSQLS>
    {
        public void Configure(EntityTypeBuilder<PersonSQLS> builder)
        {
            builder.Property(person => person.FirstName)
                .HasMaxLength(100);

            builder.Property(person => person.LastName)
                .HasMaxLength(100);

            builder.Property(person => person.StreetName)
                .HasMaxLength(200);

            builder.Property(person => person.PostalCode)
                .HasMaxLength(20);

            builder.Property(person => person.CityName)
                .HasMaxLength(100);

        }
    }
}