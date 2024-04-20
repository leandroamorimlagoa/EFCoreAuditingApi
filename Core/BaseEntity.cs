using System.ComponentModel.DataAnnotations;

namespace AuditingApi.Core
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
