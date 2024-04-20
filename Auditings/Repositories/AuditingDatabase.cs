using AuditingApi.Auditings.Model;
using AuditingApi.Core;

namespace AuditingApi.Auditings.Repositories
{
    public class AuditingDatabase
    {
        private Dictionary<string, List<AuditedEntity>> _AuditedEntities = new Dictionary<string, List<AuditedEntity>>();

        public void RegisterAuditedEntity(AuditedEntity auditedEntity)
        {
            if (!_AuditedEntities.ContainsKey(auditedEntity.EntityName))
            {
                _AuditedEntities.Add(auditedEntity.EntityName, new List<AuditedEntity>());
            }

            _AuditedEntities[auditedEntity.EntityName].Add(auditedEntity);
        }

        public IEnumerable<AuditedEntity> AuditedEntity<T>() where T : AuditableEntity
        {
            return AuditedEntity(typeof(T).Name);
        }

        public IEnumerable<AuditedEntity> AuditedEntity(string entityName)
        {
            if (_AuditedEntities.ContainsKey(entityName))
            {
                return _AuditedEntities[entityName];
            }

            return Enumerable.Empty<AuditedEntity>();
        }
    }
}
