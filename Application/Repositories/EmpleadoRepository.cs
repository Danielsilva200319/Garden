using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Persistence.Data;

namespace Application.Repositories
{
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
    {
        private readonly GardenContext _context;
        public EmpleadoRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        public Task<IQueryable<string>> getConsultasRequeridas3()
        {
            var consulta = from empleado in _context.Empleados
                           where empleado.CodigoJefe == 7
                           select empleado.Nombre + " " + empleado.Apellido1 + " " + empleado.Apellido2 + " " + empleado.Email;

        return Task.FromResult(consulta);
        }

        public Task<IQueryable<string>> getConsultasRequeridas4()
        {
            var consulta = from empleado in _context.Empleados
                           where empleado.Puesto == "Director General"
                           select empleado.Puesto + " " + empleado.Nombre + " " + empleado.Apellido1+ " " + empleado.Apellido2 + " " + empleado.Email;

        return Task.FromResult(consulta);
        }

        public Task<IQueryable<string>> getConsultasRequeridas5()
        {
            var consulta = from empleado in _context.Empleados
                           where empleado.Puesto != "Representante Ventas"
                           select empleado.Nombre + " " + empleado.Apellido1+ " " + empleado.Apellido2 + " " + empleado.Email;

        return Task.FromResult(consulta);
        }

        public Task<int> getConsultasResumen1()
        {
            var consulta = _context.Empleados.Count();
            return Task.FromResult(consulta);
        }
    }
}