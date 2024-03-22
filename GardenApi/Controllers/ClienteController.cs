using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using GardenApi.Dtos;
using Microsoft.AspNetCore.Mvc;
// Si genera error me avisan.
namespace GardenApi.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            var clientes = await _unitOfWork.Clientes.GetAllAsync();
            return _mapper.Map<List<ClienteDto>>(clientes);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return _mapper.Map<ClienteDto>(cliente);
        }
                

        

        // GET: api/cliente/consultas-requeridas/{id?}
        [HttpGet("consultas-requeridas/{id?}")]
        public async Task<IActionResult> GetConsultasRequeridas(int? id)
        {
            IQueryable<string> consultas;

            if (id.HasValue)
            {
                // Lógica para seleccionar consultas basadas en el ID proporcionado
                switch (id.Value)
                {
                    case 6:
                        consultas = await _unitOfWork.Clientes.getConsultasRequeridas6().ConfigureAwait(false);
                        break;
                    
                    // Agregar más casos según sea necesario
                    default:
                        return BadRequest("ID de consulta no válido");
                }
            }
            else
            {
                return BadRequest("Se requiere un ID de consulta");
            }

            return Ok(consultas);
        } 

        // GET: api/cliente/consultas-resumen2
        [HttpGet("consultas-resumen2")]
        public async Task<IActionResult> getConsultasResumen2()
        {
            var consultas = await _unitOfWork.Clientes.getConsultasResumen2().ConfigureAwait(false);
            return Ok(consultas);
        }

        // GET: api/cliente/consultas-resumen8
        [HttpGet("consultas-resumen8")]
        public async Task<IActionResult> getConsultasResumen8()
        {
            var consultas = await _unitOfWork.Clientes.getConsultasResumen8().ConfigureAwait(false);
            return Ok(consultas);
        }
        
         // GET: api/cliente/consultas-multitabla-interna1
        [HttpGet("consultas-multitabla-interna1")]
        public async Task<IEnumerable<ClienteByRepresentante>> GetConsultasMultitablaInterna1()
        {
            var clientes = await _unitOfWork.Clientes.GetAllAsync();
            var empleados = await _unitOfWork.Empleados.GetAllAsync();

            var consultas = from cliente in clientes
                            join empleado in empleados
                            on cliente.CodigoEmpleadoRepVentas equals empleado.CodigoEmpleado
                            select new ClienteByRepresentante
                            {
                                NombreCliente = cliente.NombreCliente,
                                NombreEmpleado = empleado.Nombre,
                                ApellidoEmpleado = empleado.Apellido1
                            };

            return consultas;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Post(ClienteDto clienteDto)
        {
            var clientes = _mapper.Map<Cliente>(clienteDto);
            _unitOfWork.Clientes.Add(clientes);
            await _unitOfWork.SaveAsync();
            if (clientes == null)
            {
                return BadRequest();
            }
            clienteDto.CodigoCliente = clientes.CodigoCliente;
            return CreatedAtAction(nameof(Post), new { id = clienteDto.CodigoCliente }, clienteDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto clienteDto)
        {
            if (clienteDto.CodigoCliente == 0)
            {
                clienteDto.CodigoCliente = id;
            }
            if (clienteDto.CodigoCliente != id)
            {
                return NotFound();
            }
            var cliente = _mapper.Map<Cliente>(clienteDto);
            clienteDto.CodigoCliente = cliente.CodigoCliente;
            _unitOfWork.Clientes.Update(cliente);
            await _unitOfWork.SaveAsync();
            return clienteDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var clientes = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            _unitOfWork.Clientes.Remove(clientes);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}