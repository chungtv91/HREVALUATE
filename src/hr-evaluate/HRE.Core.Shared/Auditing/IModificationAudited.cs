namespace HRE.Core.Shared.Auditing
{
    public interface IModificationAudited : IHasModificationTime
    {
        int? LastModifierUserId { get; set; }
    }
}
