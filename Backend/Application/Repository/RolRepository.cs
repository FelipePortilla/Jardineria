using Domain.entities;
using Domain.Interfaces;
using persistense;
using persistense.Data;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly JardineriaContext _context;

    public RolRepository(JardineriaContext context) : base(context)
    {
       _context = context;
    }
}