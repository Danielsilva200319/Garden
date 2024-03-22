using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICliente :IGenericRepository<Cliente>
    {
        public Task<IQueryable<string>> getConsultasRequeridas6();
        public Task<IQueryable<string>> getConsultasResumen2();
        public Task<IQueryable<string>> getConsultasResumen8();
        public Task<IQueryable<int>> getConsultaResume11();
        public Task<(decimal PrecioMasCaro, decimal PrecioMasBarato)> getConsultasResumen5();

        
    }
}