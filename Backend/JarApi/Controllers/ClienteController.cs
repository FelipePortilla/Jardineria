using System.Collections;
using persistense.Data;
using Domain.entities;
using Microsoft.AspNetCore.Mvc;
using ApiApolo.Controllers;
using Domain.Interfaces;
using AutoMapper;
using JarApi.Dtos;

namespace JarApi.Controllers;
public class ClienteController : BaseController
{
    private readonly IUnitOfWork _unitofwork;
    private readonly IMapper _mapper;
    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitofwork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> get()
    {
        var clientes = await _unitofwork.Clientes.GetAllAsync();

        return _mapper.Map<List<ClienteDto>>(clientes);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> Get(int id)
    {
        var cliente = await _unitofwork.Clientes.GetByIdAsync(id);
        if (cliente == null)
            return NotFound();

        return _mapper.Map<ClienteDto>(cliente);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Post(ClienteDto clienteDto)
    {
        var cliente = _mapper.Map<Cliente>(clienteDto);
        _unitofwork.Clientes.Add(cliente);
        await _unitofwork.SaveAsync();
        if (cliente == null)
        {
            return BadRequest();
        }
        clienteDto.CodigoCliente = cliente.CodigoCliente;
        return CreatedAtAction(nameof(Post), new { id = clienteDto.CodigoCliente }, clienteDto);
    }
[HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto clienteDto)
        {
            if (clienteDto == null)
                return NotFound();

            var cliente = await _unitofwork.Clientes.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            var cliente1 = _mapper.Map<Cliente>(clienteDto);
            _unitofwork.Clientes.Update(cliente1);
            await _unitofwork.SaveAsync();
            return clienteDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _unitofwork.Clientes.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            _unitofwork.Clientes.Remove(cliente);
            await _unitofwork.SaveAsync();

            return NoContent();
        }
}