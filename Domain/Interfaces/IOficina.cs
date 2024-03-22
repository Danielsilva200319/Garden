using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOficina :IGenericRepository<Oficina>
    {
        Task<IQueryable<string>> getConsultasRequeridas1();
        Task<IQueryable<string>> getConsultasRequeridas2();
    }
}