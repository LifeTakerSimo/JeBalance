using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JeBalance.SQLLite.Configuration
{
    internal class PersonConfiguration : IEntityTypeConfiguration<PersonSQLS>
    {
        public void Configure(EntityTypeBuilder<PersonSQLS> builder)
        {
            builder.ToTable("PERSON")
                .HasKey(person => person.Id);

            builder.Property(person => person.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(person => person.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(person => person.StreetNumber)
                .IsRequired();

            builder.Property(person => person.StreetName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(person => person.PostalCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(person => person.CityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(person => person.IsVIP)
                .IsRequired();
        }
    }
}