using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Domain.Interfaces;
using persistense.Data;

namespace Application.Repository
{
    public class PagoRepository : GenericRepository<Pago>, IPago
    {
        public PagoRepository(JardineriaContext context) : base(context)
        {
        }
    }
}