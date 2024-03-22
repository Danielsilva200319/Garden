using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories
{
    public class ClienteRepository :GenericRepository<Cliente>, ICliente
    {

        private readonly GardenContext _context;
        public ClienteRepository(GardenContext context) : base(context)
        {
            _context = context;
            
        }
        public Task<(decimal PrecioMasCaro, decimal PrecioMasBarato)> getConsultasResumen5()
        {
            var consulta = (
                from producto in _context.Productos
                select producto.PrecioVenta
            );

            decimal precioMasCaro = consulta.Max();
            decimal precioMasBarato = consulta.Min();

            return Task.FromResult((precioMasCaro, precioMasBarato));
        }

        public Task<IQueryable<string>> getConsultasRequeridas6()
        {
            var consulta = from cliente in _context.Clientes
                        where cliente.Pais == "Spain"
                        select cliente.NombreCliente;

        return Task.FromResult(consulta);
        }

        public Task<IQueryable<string>> getConsultasResumen2()
        {
            var consulta = from cliente in _context.Clientes
                        group cliente by cliente.Pais into grupoPais
                        select grupoPais.Key + " " + grupoPais.Count();
                        
            return Task.FromResult(consulta);
        }


        public Task<IQueryable<string>> getConsultasResumen8()
        {
            var consulta = from cliente in _context.Clientes
                                    where cliente.Ciudad.StartsWith("M")
                                    group cliente by cliente.Ciudad into grupoCiudad
                                    select grupoCiudad.Key + " " + grupoCiudad.Count();
            return Task.FromResult(consulta);
        }


        public Task<IQueryable<int>> getConsultaResume11()
        {
            var consulta = from cliente in _context.Clientes
                        join pago in _context.Pagos
                        on cliente.CodigoCliente equals pago.CodigoCliente
                        into pagosPorCliente
                        select pagosPorCliente.Count();
            return Task.FromResult(consulta);
        }

    
    }
}