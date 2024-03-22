using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories
{
    public class PagoRepository : GenericRepository<Pago>, IPago
    {
        private readonly GardenContext _context;
        public PagoRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        

    






        public Task<IQueryable<int>> getConsultasRequeridas8()
        {
            var consulta = from pago in _context.Pagos
                        where pago.FechaPago.Year == 2008
                        select pago.CodigoCliente;

        return Task.FromResult(consulta);
        }

        

    }
}