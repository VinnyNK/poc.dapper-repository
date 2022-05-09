using poc.dapper_repository.Domain.Entities;

namespace poc.dapper_repository.Domain.Interfaces;

public interface IUserService
{   
    Task<User?> AddUser(User user);
    Task<User?> GetUserById(Guid id);
    Task<IEnumerable<User>> GetAllUsers();
    Task<bool> RemoveUser(Guid id);
    Task<bool> UpdateUser(User newUser, Guid id);
}
