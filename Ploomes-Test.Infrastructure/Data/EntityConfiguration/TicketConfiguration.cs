using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ploomes_Test.Domain;

namespace Ploomes_Test.Infrastructure.Data.TableDefinition;

public class TicketConfiguration: IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.RequesterEmail)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.AssigneeEmail)
            .IsRequired(false)
            .HasMaxLength(255);

        builder.Property(t => t.Subject)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(t => t.Description)
            .IsRequired();

        //  Map Ticket Status Enum into String
        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();
        
        // Map DateTimeOffset properties
        builder.Property(t => t.CreateDate)
            .IsRequired()
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.UpdateDate)
            .IsRequired()
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.StartedAt)
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.EndedAt)
            .HasColumnType("datetimeoffset");
    }
}