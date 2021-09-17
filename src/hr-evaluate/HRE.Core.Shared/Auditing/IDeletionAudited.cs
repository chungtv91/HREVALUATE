using System;

namespace HRE.Core.Shared.Auditing
{
    public interface IDeletionAudited
    {
        bool IsDeleted { get; set; }

        int? DeleterUserId { get; set; }

        DateTime? DeletionTime { get; set; }
    }
}
