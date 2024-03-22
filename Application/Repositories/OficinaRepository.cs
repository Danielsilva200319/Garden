using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories
{
    public class OficinaRepository : GenericRepository<Oficina>, IOficina
    {
        private readonly GardenContext _context;
        public OficinaRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        public Task<IQueryable<string>> getConsultasRequeridas1()
        {
            var consulta = from oficina in _context.Oficinas
                        select oficina.CodigoOficina + " " + oficina.Ciudad;

        return Task.FromResult(consulta);
        }

        public Task<IQueryable<string>> getConsultasRequeridas2()
        {
            var consulta = from oficina in _context.Oficinas
                        where oficina.Pais == "España"
                        select oficina.Ciudad + " " + oficina.Telefono;

        return Task.FromResult(consulta);
        }
    }
}