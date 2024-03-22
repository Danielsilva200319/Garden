using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProducto :IGenericRepository<Producto>
    {
        public Task<(decimal PrecioMasCaro, decimal PrecioMasBarato)> getConsultasResumen05();

        public Task<int> getConsultaResumen14();
        
    }
}