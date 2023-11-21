using Domain.entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using persistense;
using persistense.Data;
namespace Application.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly JardineriaContext _context;

    public UserRepository(JardineriaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
            .Include(u => u.Rols)
            .Include(u => u.Refreshtokens)
            .FirstOrDefaultAsync(u => u.Refreshtokens.Any(t => t.Token == refreshToken));
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Rols)
            .Include(u => u.Refreshtokens)
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }
}