
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using JarApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.entities;
using ApiApolo.Controllers;

namespace JarApi.Controllers
{
    public class ClienteController : BaseController
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
            var cliente = await _unitOfWork.Clientes.GetByIdInt(id);
            if (cliente == null)
                return NotFound();

            return _mapper.Map<ClienteDto>(cliente);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Post(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            _unitOfWork.Clientes.Add(cliente);
            await _unitOfWork.SaveAsync();

            if (cliente == null)
            {
                return BadRequest();
            }

            clienteDto.CodigoCliente = cliente.CodigoCliente;  // Ajusta según la propiedad de identificación de tu entidad Jefe

            return CreatedAtAction(nameof(Post), new { id = clienteDto.CodigoCliente }, clienteDto);
        }


    }
}