using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuditingApi.Auditings.Model
{
    public class AuditedChanges
    {
        [Key]
        [JsonIgnore]
        public long Id { get; set; }
        public string PropertyName { get; set; }
        public string OriginalValue { get; set; }
        public string CurrentValue { get; set; }

        public AuditedChanges() { }
    }
}
