using AuditingApi.Auditings.Model;
using AuditingApi.Auditings.Repositories;
using AuditingApi.Core;

namespace AuditingApi.Auditings.Services
{
    public class AuditingService
    {
        private readonly AuditingDatabase auditingDatabase;

        public AuditingService(AuditingDatabase auditingDatabase)
        {
            this.auditingDatabase = auditingDatabase;
        }

        public void RegisterAuditing(AuditedEntity auditEntry)
        {
            Task.Run(() =>
            {
                var auditableProperties = typeof(AuditableEntity).GetProperties();
                auditEntry.Changes = auditEntry.Changes.FindAll(c => !auditableProperties.Any(p => p.Name == c.PropertyName));

                this.auditingDatabase.RegisterAuditedEntity(auditEntry);
            });
        }

        public AuditedEntity? GetAuditedFromEntity(string entityName, Guid id)
        => this.auditingDatabase.AuditedEntity(entityName).FirstOrDefault(p => p.Id == id);

        public IEnumerable<AuditedEntity> GetFromEntity(string entityName)
        => this.auditingDatabase.AuditedEntity(entityName);
    }
}
