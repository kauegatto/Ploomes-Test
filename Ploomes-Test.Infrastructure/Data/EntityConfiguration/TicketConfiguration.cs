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
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.Subject)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(t => t.Description)
            .IsRequired();

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();
        
       
    }
}