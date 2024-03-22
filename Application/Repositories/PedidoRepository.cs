using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedido
    {
        private readonly GardenContext _context;
        public PedidoRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        public Task<IQueryable<string>> getConsultasRequeridas7()
        {
            var consulta = (from pedido in _context.Pedidos
                            select pedido.Estado)
                        .Distinct();

            return Task.FromResult(consulta);
        }

        public Task<IQueryable<string>> getConsultasRequeridas9()
        {
            var consulta = from pedido in _context.Pedidos
                        where pedido.FechaEsperada < pedido.FechaEntrega
                        select pedido.CodigoPedido + " " + pedido.CodigoCliente + " " + pedido.FechaEsperada + " " + pedido.FechaEntrega;

            return Task.FromResult(consulta);
        }

        public Task<IQueryable<string>> getConsultasRequeridas10()
        {
            var consulta = from pedido in _context.Pedidos
                        where EF.Functions.DateDiffDay(pedido.FechaEsperada, pedido.FechaEntrega) < 2
                        select pedido.CodigoPedido + " " + pedido.CodigoCliente + " " + pedido.FechaEsperada + " " + pedido.FechaEntrega;

            return Task.FromResult(consulta);
        }

        public Task<IQueryable<string>> getConsultasRequeridas11()
        {
            var consulta = from pedido in _context.Pedidos
                        where pedido.Estado == "Rechazado" && pedido.FechaPedido.Value.Year == 2009
                        select pedido.CodigoPedido + " " + pedido.CodigoCliente + " " + pedido.FechaEsperada + " " + pedido.FechaEntrega;

            return Task.FromResult(consulta);
        }

        public Task<IQueryable<string>> getConsultasRequeridas12()
        {
            var consulta = from pedido in _context.Pedidos
                        where pedido.Estado == "Entregado" && pedido.FechaEntrega.Value.Month == 1
                        select pedido.CodigoPedido + " " + pedido.CodigoCliente + " " + pedido.FechaEsperada + " " + pedido.FechaEntrega;

            return Task.FromResult(consulta);
        }













        public Task<IQueryable<int>> getConsultaResume12()
        {
            var consulta = from pedido in _context.Pedidos
                        join detalle in _context.DetallePedidos
                        on pedido.CodigoPedido equals detalle.CodigoPedido
                        into detallePorPedidos
                           select pedido.CodigoPedido + detallePorPedidos
                                                               .Select(dp => dp.CodigoPedido)
                                                               .Distinct()
                                                                           .Count();
            return Task.FromResult(consulta);
        }


        public Task<IQueryable<int>> getConsultaResumen13()
        {
            var consulta = from pedido in _context.Pedidos
                        join detalle in _context.DetallePedidos
                        on pedido.CodigoPedido equals detalle.CodigoPedido
                        into detallePedidos
                        select pedido.CodigoPedido + detallePedidos
                                                    .Sum(dp => dp.Cantidad);
            return Task.FromResult(consulta);
        }



    }
}