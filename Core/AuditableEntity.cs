using System.Text.Json.Serialization;

namespace AuditingApi.Core
{
    public class AuditableEntity : BaseEntity
    {
        [JsonIgnore]
        public DateTime Modified { get; private set; } = DateTime.UtcNow;

        public AuditableEntity()
        {
            Modified = DateTime.UtcNow;
        }
    }
}
