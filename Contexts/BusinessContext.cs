using AuditingApi.Auditings.Model;
using AuditingApi.Auditings.Services;
using AuditingApi.Core;
using AuditingApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuditingApi.Contexts
{
    public class BusinessContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Book> Book { get; set; }

        private readonly AuditingService auditingService;

        public BusinessContext(DbContextOptions<BusinessContext> options, AuditingService auditingService) : base(options)
        {
            this.auditingService = auditingService;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var auditableEntry = entry.Entity as AuditableEntity;
                if (auditableEntry == null || !this.ShouldAuditEntry(entry))
                {
                    continue;
                }

                var auditEntry = new AuditedEntity(entry);

                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;
                    if (propertyName == "Id")
                    {
                        auditEntry.SetEntityId(property.CurrentValue);
                    }

                    if (property.IsModified || entry.State == EntityState.Added || entry.State == EntityState.Deleted)
                    {
                        var originalValue = entry.State == EntityState.Added ? string.Empty : property.OriginalValue?.ToString();
                        var currentValue = entry.State == EntityState.Deleted ? string.Empty : property.CurrentValue?.ToString();

                        auditEntry.Changes.Add(new AuditedChanges
                        {
                            PropertyName = propertyName,
                            OriginalValue = originalValue ?? string.Empty,
                            CurrentValue = currentValue ?? string.Empty
                        });
                    }
                }

                if (auditEntry.Changes.Count > 0)
                    this.auditingService.RegisterAuditing(auditEntry);

            }
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private bool ShouldAuditEntry(EntityEntry entry)
        => (entry.State == EntityState.Added
            || entry.State == EntityState.Modified
            || entry.State == EntityState.Deleted);
    }
}
