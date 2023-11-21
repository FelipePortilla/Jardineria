using Domain.entities;

namespace Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User> 
{ 
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByRefreshTokenAsync(string username);

}