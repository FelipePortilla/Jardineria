using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRolRepository Roles {get;}
        IUserRepository Users { get; }
        ICliente ? Clientes{get;}
        IDetallePedido ?DetallePedidos{get;}
        IEmpleado ?Empleados{get;}
        IGamaProducto ?GamaProductos {get;}
        IOficina ?Oficinas {get;}
        IPago ?Pagos{get;}
        IPedido ?Pedidos {get;}
        IProducto Productos {get;}
        IJefe Jefes {get;}
        Task<int> SaveAsync();
    }
}