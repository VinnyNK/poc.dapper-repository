using poc.dapper_repository.Domain.Entities;

namespace poc.dapper_repository.Domain.interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<bool> Add(TEntity entity);

    Task<bool> Remove(Guid id);

    Task<bool> Update(TEntity newEntity, Guid? id);

    Task<TEntity?> GetById(Guid id);

    Task<IEnumerable<TEntity>> GetAll();
}
