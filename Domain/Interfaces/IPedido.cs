using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPedido :IGenericRepository<Pedido>
    {
        public Task<IQueryable<string>> getConsultasRequeridas7();
        public Task<IQueryable<string>> getConsultasRequeridas9();
        public Task<IQueryable<string>> getConsultasRequeridas10();
        public Task<IQueryable<string>> getConsultasRequeridas11();
        public Task<IQueryable<string>> getConsultasRequeridas12();
        public Task<IQueryable<int>> getConsultaResumen13();

    
    }
}