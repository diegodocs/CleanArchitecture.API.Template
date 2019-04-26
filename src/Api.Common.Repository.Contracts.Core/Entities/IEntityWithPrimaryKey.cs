namespace Api.Common.Repository.Contracts.Core.Entities
{
    public interface IEntityWithPrimaryKey<TId>
    {
        TId Id { get; set; }
    }
}