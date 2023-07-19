using Jadval.Domain.Common;

namespace Jadval.Domain.OutBoxEventItems.Entities
{
    public class OutBoxEventItem : AuditableBaseEntity
    {
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public string EventPayload { get; set; }
    }
}
