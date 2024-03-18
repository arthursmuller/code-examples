namespace Notifications.Domain.Abstractions
{
    public interface IRepositoryBase<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
