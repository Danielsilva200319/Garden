using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        private readonly GardenContext _context;
        public ProductoRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        public Task<(decimal PrecioMasCaro, decimal PrecioMasBarato)> getConsultasResumen05()
            {
                var consulta = (
                    from producto in _context.Productos
                    select producto.PrecioVenta
                );

                decimal precioMasCaro = consulta.Max();
                decimal precioMasBarato = consulta.Min();

                return Task.FromResult((precioMasCaro, precioMasBarato));
            }




        public Task<int> getConsultaResumen14()
        {
            var consulta = from producto in _context.Productos
                        join detalle in _context.DetallePedidos
                        on producto.CodigoProducto equals detalle.CodigoProducto
                        into detallesProducto
                        select detallesProducto.Sum(dp => dp.Cantidad);
            int resultado = consulta.FirstOrDefault();
            return Task.FromResult(resultado);
        }
    }
}