namespace ECommerce.Domain.Primitives;

public interface IBaseEntity
{
    Guid Id { get; protected set; }
}