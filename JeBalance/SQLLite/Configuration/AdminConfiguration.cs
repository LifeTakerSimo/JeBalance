using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JeBalance.SQLLite.Configuration
{
    internal class AdminConfiguration : IEntityTypeConfiguration<AdminSQLS>
    {
        public void Configure(EntityTypeBuilder<AdminSQLS> builder)
        {
        }
    }
}