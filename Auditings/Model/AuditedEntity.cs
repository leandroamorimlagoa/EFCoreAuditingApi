using System.ComponentModel.DataAnnotations;
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
        public List<AuditedChanges> Changes { get; set; } = new List<AuditedChanges>();

        public AuditedEntity(EntityEntry entry)
        {
            Id = Guid.NewGuid();
            OperationName = entry.State.ToString();
            EntityName = entry.Metadata.DisplayName();
            Modified = DateTime.UtcNow;
        }

        public void SetEntityId(object? ValueId)
        {
            this.EntityId = ValueId?.ToString() ?? string.Empty;
        }
    }
}
