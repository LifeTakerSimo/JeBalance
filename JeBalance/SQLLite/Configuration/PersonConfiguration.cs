using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JeBalance.SQLLite.Configuration
{
    internal class PersonConfiguration : IEntityTypeConfiguration<PersonSQLS>
    {
        public void Configure(EntityTypeBuilder<PersonSQLS> builder)
        {
            builder.Property(Person => Person.FirstName)
                .HasMaxLength(100);

            builder.Property(Person => Person.LastName)
                .HasMaxLength(100);

            builder.Property(Person => Person.StreetName)
                .HasMaxLength(200);

            builder.Property(Person => Person.PostalCode)
                .HasMaxLength(20);

            builder.Property(Person => Person.CityName)
                .HasMaxLength(100);

        }
    }
}