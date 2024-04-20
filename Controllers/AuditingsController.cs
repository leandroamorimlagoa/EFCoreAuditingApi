using AuditingApi.Auditings.Repositories;
using AuditingApi.Auditings.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuditingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditingsController : ControllerBase
    {
        private readonly AuditingService auditingService;

        public AuditingsController(AuditingService auditingService)
        {
            this.auditingService = auditingService;
        }

        [HttpGet("/{entityName}")]
        public IActionResult Get(string entityName)
        {
            return Ok(this.auditingService.GetFromEntity(entityName));
        }

        [HttpGet("/{entityName}/{id}")]
        public IActionResult Get(string entityName, Guid id)
        {
            var auditedEntity = this.auditingService.GetAuditedFromEntity(entityName, id);
            if (auditedEntity == null)
            {
                return NotFound();
            }
            return Ok(auditedEntity);
        }
    }
}
