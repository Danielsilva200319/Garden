using Application.Repositories;
using Domain.Interfaces;
using Persistence.Data;



namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly GardenContext _context;
        private ClienteRepository _clientes;
        private DetallePedidoRepository _detallepedidos;
        private EmpleadoRepository _empleados;
        private GamaProductoRepository _gamaproductos;
        private OficinaRepository _oficinas;
        private PagoRepository _pagos;
        private PedidoRepository _pedidos;
        private ProductoRepository _productos;
        private RefreshTokenRepository _refreshtokens;
        private RolRepository _roles;
        private UserRepository _users;

        public UnitOfWork(GardenContext context)
        {
            _context = context;
        }

        public ICliente Clientes
        {
            get
            {
                if(_clientes == null)
                {
                    _clientes = new ClienteRepository(_context);
                }
                return _clientes;
            }
        }

        public IDetallePedido DetallePedidos
        {
            get
            {
                if(_detallepedidos == null)
                {
                    _detallepedidos = new DetallePedidoRepository(_context);
                }
                return _detallepedidos;
            }
        }

        public IEmpleado Empleados
        {
            get
            {
                if(_empleados == null)
                {
                    _empleados = new EmpleadoRepository(_context);
                }
                return _empleados;
            }
        }

        public IGamaProducto GamaProductos
        {
            get
            {
                if(_gamaproductos == null)
                {
                    _gamaproductos = new GamaProductoRepository(_context);
                }
                return _gamaproductos;
            }
        }

        public IOficina Oficinas
        {
            get
            {
                if(_oficinas == null)
                {
                    _oficinas = new OficinaRepository(_context);
                }
                return _oficinas;
            }
        }

        public IPago Pagos
        {
            get
            {
                if(_pagos == null)
                {
                    _pagos = new PagoRepository(_context);
                }
                return _pagos;
            }
        }

        public IPedido Pedidos
        {
            get
            {
                if(_pedidos == null)
                {
                    _pedidos = new PedidoRepository(_context);
                }
                return _pedidos;
            }
        }

        public IProducto Productos
        {
            get
            {
                if(_productos == null)
                {
                    _productos = new ProductoRepository(_context);
                }
                return _productos;
            }
        }

        public IRefreshToken RefreshTokens
        {
            get
            {
                if (_refreshtokens == null)
                {
                    _refreshtokens = new RefreshTokenRepository(_context);
                }
                return _refreshtokens;
            }
        }

        public IRol Rols
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolRepository(_context);
                }
                return _roles;
            }
        }

        public IUser Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}