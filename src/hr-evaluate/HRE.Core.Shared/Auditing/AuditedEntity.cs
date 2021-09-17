using System;

namespace HRE.Core.Shared.Auditing
{
    public abstract class AuditedEntity: IAudited
    {
        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
    }
}
