namespace poc.dapper_repository.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }
}
