using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmpleado :IGenericRepository<Empleado>
    {
        public Task<IQueryable<string>> getConsultasRequeridas3();
        public Task<IQueryable<string>> getConsultasRequeridas4();
        public Task<IQueryable<string>> getConsultasRequeridas5();
        public Task<int> getConsultasResumen1();
    }
}