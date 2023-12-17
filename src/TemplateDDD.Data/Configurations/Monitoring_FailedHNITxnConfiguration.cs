using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateDDD.Data.Entities;

namespace TemplateDDD.Data.Configurations
{
    public class MonitoringFailedHNITxnConfiguration : IEntityTypeConfiguration<MonitoringFailedHNITxn>
    {
        public void Configure(EntityTypeBuilder<MonitoringFailedHNITxn> builder)
        {
            builder.ToTable("MonitoringFailedHNITxns", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.AccountNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(t => t.AccountName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(t => t.UserMessage)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(t => t.PhoneNo)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}