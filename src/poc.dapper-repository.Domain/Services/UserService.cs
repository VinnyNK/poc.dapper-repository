using poc.dapper_repository.Domain.Entities;
using poc.dapper_repository.Domain.interfaces;
using poc.dapper_repository.Domain.Interfaces;

namespace poc.dapper_repository.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> AddUser(User user)
    {
        var result = await _userRepository.Add(user);

        return result ? user : null;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAll();
    }

    public async Task<bool> RemoveUser(Guid id)
    {
        return await _userRepository.Remove(id);
    }

    public async Task<bool> UpdateUser(User newUser, Guid id)
    {
        return await _userRepository.Update(newUser, id);
    }
}
