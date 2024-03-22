# Garden

# Proyecto Garden.

## Creacion de proyecto.
Utilizamos el metodo .bat el cual nos crea el proyecto con todas las carpetas, referencias, soluciones, entre otras..

## Creacion Entities y DbContext con DbFirts.
## Codigo que se debe usar para la creacion de entidades, configuraciones y Context del proyecto.
Con el DbFirts tambien podemos crear desde consola el archivo de 


dotnet ef dbcontext scaffold "server=localhost;user=root;password=Helpsystem2020*;database=jardineria;" Pomelo.EntityFrameworkCore.MySqql -s GardenApi -p Persistence --context GardenContext --context-dir Data --output-dir Entities


Despues de estos, nos encontraremos las Entidades y configuraciones en la carpeta de Persistencia en el archivo de DbContext, debemos mover las entidades y configuraciones a su respectiva carpeta al igual que "server=localhost;user=root;password=Helpsystem2020*;database=jardineria;" en el archio Programs.cs


# Interfaces.
```
Procedemos a la creacion de interfaces, IUnitOfWork, IGenericRepository

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
```


## IUnitOfWork
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICliente Clientes {get; }
        IDetallePedido DetallePedidos {get; }
        IEmpleado Empleados {get; }
        IGamaProducto GamaProductos {get;}
        IOficina Oficinas {get;}
        IPago Pagos {get;}
        IPedido Pedidos {get;}
        IProducto Productos {get;}
        IRefreshToken RefreshTokens {get;}
        IRol Rols {get; }
        IUser Users {get; }


        Task<int> SaveAsync();
    }
}
```


## IGenericRepository	

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<T> GetByIdAsync(string stringId);
        Task<T> GetByIdAsync(int intId);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        
    }
}
```


# Repositorio Generico, Repositorios.
## GenericRepository
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GardenContext _context;
        public GenericRepository(GardenContext context)
        {
            _context = context;
        }
        public virtual void  Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().AddRange(entity);
        }
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(
            int pageIndex,
            int pageSize, 
            string search)
        {
            var totalRegistros = await _context.Set<T>().CountAsync();
                var registros = await _context
                .Set<T>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRegistros, registros);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(string id )
        {
            return await _context.Set<T>().FindAsync(id);
            
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>()
                .Update(entity);

        }
    }
}
```

## Repositorios
```
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
```


# EndPoints
CLIENTE

GET
http://localhost:5066/api/Cliente

POST
http://localhost:5066/api/Cliente

GET
http://localhost:5066/api/Cliente/{id}

PUT
http://localhost:5066/api/Cliente/{id}

DELETE
http://localhost:5066/api/Cliente/{id}

GET
http://localhost:5066/api/Cliente/consultas-requeridas/{id}

GET
http://localhost:5066/api/Cliente/consultas-resumen2

GET
http://localhost:5066/api/Cliente/consultas-resumen8

GET
http://localhost:5066/api/Cliente/consultas-multitabla-interna1


DETALLE PEDIDO

GET
 http://localhost:5066/api/DetallePedido

POST
 http://localhost:5066/api/DetallePedido

GET
 http://localhost:5066/api/DetallePedido/{id}

PUT
 http://localhost:5066/api/DetallePedido/{id}

DELETE
 http://localhost:5066/api/DetallePedido/{id}


EMPLEADO

GET
 http://localhost:5066/api/Empleado

POST
 http://localhost:5066/api/Empleado

GET
 http://localhost:5066/api/Empleado/{id}

PUT
 http://localhost:5066/api/Empleado/{id}

DELETE
 http://localhost:5066/api/Empleado/{id}

GET
 http://localhost:5066/api/Empleado/consultas-requeridas3

GET
 http://localhost:5066/api/Empleado/consultas-requeridas4

GET
 http://localhost:5066/api/Empleado/consultas-requeridas5

GET
 http://localhost:5066/api/Empleado/consultas-resumen1

 GAMAPRODUCTO

 GET
http://localhost:5066/api/GamaProducto

POST
http://localhost:5066/api/GamaProducto

GET
http://localhost:5066/api/GamaProducto/{id}

PUT
http://localhost:5066/api/GamaProducto/{id}

DELETE
http://localhost:5066/api/GamaProducto/{id}


OFICINA 

GET
http://localhost:5066/api/Oficina

POST
http://localhost:5066/api/Oficina

GET
http://localhost:5066/api/Oficina/{id}

PUT
http://localhost:5066/api/Oficina/{id}

DELETE
http://localhost:5066/api/Oficina/{id}

GET
http://localhost:5066/api/Oficina/consultas-requeridas/{id}


PAGO


GET
http://localhost:5066/api/Pago

POST
http://localhost:5066/api/Pago

GET
http://localhost:5066/api/Pago/{id}

PUT
http://localhost:5066/api/Pago/{id}

DELETE
http://localhost:5066/api/Pago/{id}


PEDIDO

GET
http://localhost:5066/api/Pedido

POST
http://localhost:5066/api/Pedido

GET
http://localhost:5066/api/Pedido/{id}

PUT
http://localhost:5066/api/Pedido/{id}

DELETE
http://localhost:5066/api/Pedido/{id}

GET
http://localhost:5066/api/Pedido/consultas-requeridas/{id}

GET
http://localhost:5066/http://localhost:5066/api/Pedido/consultas-resume12

GET
http://localhost:5066/api/Pedido/consultas-resume13

PRODUCTO

GET
http://localhost:5066/api/Producto

POST
http://localhost:5066/api/Producto

GET
http://localhost:5066/api/Producto/{id}

PUT
http://localhost:5066/api/Producto/{id}

DELETE
http://localhost:5066/api/Producto/{id}

GET
http://localhost:5066/api/Producto/consultas-requeridas5

GET
http://localhost:5066/api/Producto/consultas-resumen14


USER


POST
http://localhost:5066/api/User/register

POST
http://localhost:5066/api/User/token

POST
http://localhost:5066/api/User/addrol

POST
http://localhost:5066/api/User/refresh-token

