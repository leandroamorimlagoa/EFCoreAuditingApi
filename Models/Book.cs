using AuditingApi.Core;

namespace AuditingApi.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int Year { get; set; }
    }
}
