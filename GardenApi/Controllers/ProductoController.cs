using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using GardenApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GardenApi.Controllers
{
    public class ProductoController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
            var productos = await _unitOfWork.Productos.GetAllAsync();
            return _mapper.Map<List<ProductoDto>>(productos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Get(string id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProductoDto>(producto);
        }

        // GET: api/producto/consultas-requeridas5
        [HttpGet("consultas-requeridas5")]
        public async Task<IActionResult> GetConsultasResumen5()
        {
            var consultas = await _unitOfWork.Clientes.getConsultasResumen5().ConfigureAwait(false);
            return Ok(consultas);
        }


        // GET: api/oficinas/consultas-resumen14
        [HttpGet("consultas-resumen14")]
        public async Task<IActionResult> GetConsultasResumen14()
        {
            var consultas = await _unitOfWork.Productos.getConsultaResumen14().ConfigureAwait(false);
            return Ok(consultas);
        }










        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Post(ProductoDto productoDto)
        {
            var productos = _mapper.Map<Producto>(productoDto);
            _unitOfWork.Productos.Add(productos);
            await _unitOfWork.SaveAsync();
            if (productos == null)
            {
                return BadRequest();
            }
            productoDto.CodigoProducto = productos.CodigoProducto.ToString();
            return CreatedAtAction(nameof(Post), new { id = productoDto.CodigoProducto }, productoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Put(string id, [FromBody] ProductoDto productoDto)
        {
            if (productoDto.CodigoProducto == "0")
            {
                productoDto.CodigoProducto = id;
            }
            if (productoDto.CodigoProducto != id)
            {
                return NotFound();
            }
            var producto = _mapper.Map<Producto>(productoDto);
            productoDto.CodigoProducto = producto.CodigoProducto.ToString();
            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveAsync();
            return productoDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var productos = await _unitOfWork.Productos.GetByIdAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            _unitOfWork.Productos.Remove(productos);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}