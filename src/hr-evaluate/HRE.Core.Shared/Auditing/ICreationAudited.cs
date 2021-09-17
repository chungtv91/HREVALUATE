namespace HRE.Core.Shared.Auditing
{
    public interface ICreationAudited : IHasCreationTime
    {
        int? CreatorUserId { get; set; }
    }
}
