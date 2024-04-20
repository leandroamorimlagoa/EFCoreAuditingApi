using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuditingApi.Auditings.Model
{
    public class AuditedEntity
    {
        [Key]
        public Guid Id { get; }
        public string OperationName { get; }
        public string EntityName { get; }
        public string EntityId { get; private set; }
        public DateTime Modified { get; }
        public List<AuditedChanges> Changes { get; private set; }

        public AuditedEntity(EntityEntry entry)
        {
            Id = Guid.NewGuid();
            OperationName = entry.State.ToString();
            EntityName = entry.Metadata.DisplayName();
            Modified = DateTime.UtcNow;
            Changes = new List<AuditedChanges>();
            this.LoadChanges(entry);
        }

        private void LoadChanges(EntityEntry entry)
        {
            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;
                if (propertyName == "Id")
                {
                    SetEntityId(property.CurrentValue);
                }

                if (property.IsModified || entry.State == EntityState.Added || entry.State == EntityState.Deleted)
                {
                    var originalValue = entry.State == EntityState.Added ? string.Empty : property.OriginalValue?.ToString();
                    var currentValue = entry.State == EntityState.Deleted ? string.Empty : property.CurrentValue?.ToString();

                    this.Changes.Add(new AuditedChanges
                    {
                        PropertyName = propertyName,
                        OriginalValue = originalValue ?? string.Empty,
                        CurrentValue = currentValue ?? string.Empty
                    });
                }
            }
        }

        private void SetEntityId(object? ValueId)
        {
            this.EntityId = ValueId?.ToString() ?? string.Empty;
        }

        internal void CleanProperties()
        {
            throw new NotImplementedException();
        }

        internal void RemoveChangedProperties(List<string> auditableEntityProperties)
        {
            this.Changes.RemoveAll(p => auditableEntityProperties.Contains(p.PropertyName));
        }
    }
}
