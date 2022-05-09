using poc.dapper_repository.Domain.Entities;
using poc.dapper_repository.Domain.interfaces;
using poc.dapper_repository.Infra.Context;

namespace poc.dapper_repository.Infra.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbConext context) : base(context, "User")
    { }
}
