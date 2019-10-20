using AccelerometerStorage.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccelerometerStorage.Persistance.EntityFramework
{
    public sealed class DataFileConfiguration : IEntityTypeConfiguration<DataFile>
    {
        public void Configure(EntityTypeBuilder<DataFile> builder)
        {
            builder.ToTable("DataFiles");

            builder.HasKey(df => df.Id);
        }
    }
}
