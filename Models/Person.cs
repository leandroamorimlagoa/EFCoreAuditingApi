using AuditingApi.Core;

namespace AuditingApi.Models
{
    public class Person : AuditableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
