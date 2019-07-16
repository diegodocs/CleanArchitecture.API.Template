using Api.Template.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Template.DbEFCore.Mappings
{
    public class ReleaseCallStatusMapConfig : IEntityTypeConfiguration<ReleaseCallStatus>
    {
        public void Configure(EntityTypeBuilder<ReleaseCallStatus> builder)
        {
            builder.ToTable("ReleaseCallStatus");
        }
    }
}